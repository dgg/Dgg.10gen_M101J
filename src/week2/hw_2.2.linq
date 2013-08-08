<Query Kind="Program">
  <NuGetReference>mongocsharpdriver</NuGetReference>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Bson.IO</Namespace>
  <Namespace>MongoDB.Bson.Serialization</Namespace>
  <Namespace>MongoDB.Bson.Serialization.Attributes</Namespace>
  <Namespace>MongoDB.Bson.Serialization.Conventions</Namespace>
  <Namespace>MongoDB.Bson.Serialization.IdGenerators</Namespace>
  <Namespace>MongoDB.Bson.Serialization.Options</Namespace>
  <Namespace>MongoDB.Bson.Serialization.Serializers</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>MongoDB.Driver.Builders</Namespace>
  <Namespace>MongoDB.Driver.Communication.Security</Namespace>
  <Namespace>MongoDB.Driver.Communication.Security.Mechanisms.Gsasl</Namespace>
  <Namespace>MongoDB.Driver.Communication.Security.Mechanisms.Sspi</Namespace>
  <Namespace>MongoDB.Driver.GeoJsonObjectModel</Namespace>
  <Namespace>MongoDB.Driver.GeoJsonObjectModel.Serializers</Namespace>
  <Namespace>MongoDB.Driver.GridFS</Namespace>
  <Namespace>MongoDB.Driver.Internal</Namespace>
  <Namespace>MongoDB.Driver.Linq</Namespace>
  <Namespace>MongoDB.Driver.Wrappers</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	MongoCollection grades = new MongoClient()
		.GetServer().GetDatabase("students")
		.GetCollection("grades");
		
	/*System.Diagnostics.Debug.Assert(grades.Count().Equals(800));
	
	IEnumerable<IGrouping<BsonValue, BsonDocument>> byStudent = grades
		.FindAs<BsonDocument>(Query.EQ("type", "homework"))
		.GroupBy(d => d["student_id"]);
	
	foreach (IGrouping<BsonValue, BsonDocument> homeworks in byStudent)
	{
		BsonDocument minHomework = homeworks.MinBy(h => h["score"].AsDouble);
		grades.Remove(Query.EQ("_id", minHomework["_id"]));
	}
	
	System.Diagnostics.Debug.Assert(grades.Count().Equals(600));*/
	
	/*	db.grades.find().sort({'score':-1}).skip(100).limit(1) */
	BsonDocument oneOhOneth = grades.FindAs<BsonDocument>(Query.Null)
		.SetSortOrder(SortBy.Descending("score"))
		.SetSkip(100)
		.SetLimit(1)
		.Single();
	
	/*{ "_id" : ObjectId("513257f68d6e7cb63d7b1ead"), "student_id" : 164, "type" : "exam", "score" : 87.06518186605459 }*/
	System.Diagnostics.Debug.Assert(oneOhOneth["student_id"].AsInt32.Equals(164));
	

	/* db.grades.find({},{'student_id':1, 'type':1, 'score':1, '_id':0}).sort({'student_id':1, 'score':1, }).limit(5) */
	grades.FindAs<BsonDocument>(Query.Null)
		.SetFields(Fields.Exclude("_id").Include("student_id", "type", "score"))
		.SetSortOrder(SortBy.Ascending("student_id").Ascending("score"))
		.SetLimit(5)
		.Dump(2);
	/*
	{ "student_id" : 0, "type" : "quiz", "score" : 16.28337833467709 }
	{ "student_id" : 0, "type" : "exam", "score" : 64.40706888325151 }
	{ "student_id" : 0, "type" : "homework", "score" : 80.31845193864314 }
	{ "student_id" : 1, "type" : "quiz", "score" : 11.45004974085635 }
	{ "student_id" : 1, "type" : "homework", "score" : 31.56114538077717 }
	*/
	
	/* db.grades.aggregate(
		{'$group':{'_id':'$student_id', 'average':{$avg:'$score'}}},
		{'$sort':{'average':-1}},
		{'$limit':1})
	*/
	AggregateResult result = grades.Aggregate(
		new BsonDocument("$group", 
			new BsonDocument()
			{
				{"_id", "$student_id"},
				{"average", new BsonDocument("$avg", "$score")}
			}),
		new BsonDocument("$sort", new BsonDocument("average", -1)),
		new BsonDocument("$limit", 1)
		);
	BsonDocument highestAvg = result.ResultDocuments.Single();
	int answer = highestAvg["_id"].AsInt32.Dump("answer");
}

// Define other methods and classes here
static class Extensions
{
	public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
			Func<TSource, TKey> selector)
	{
		return source.MinBy(selector, Comparer<TKey>.Default);
	}
	
public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
		   Func<TSource, TKey> selector, IComparer<TKey> comparer)
	{
		using (IEnumerator<TSource> sourceIterator = source.GetEnumerator())
		{
			if (!sourceIterator.MoveNext())
			{
				throw new InvalidOperationException("Sequence was empty");
			}
			TSource min = sourceIterator.Current;
			TKey minKey = selector(min);
			while (sourceIterator.MoveNext())
			{
				TSource candidate = sourceIterator.Current;
				TKey candidateProjected = selector(candidate);
				if (comparer.Compare(candidateProjected, minKey) < 0)
				{
					min = candidate;
					minKey = candidateProjected;
				}
			}
			return min;
		}
	}
}