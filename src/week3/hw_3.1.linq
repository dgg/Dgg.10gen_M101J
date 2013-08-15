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
	MongoCollection students = new MongoClient()
		.GetServer().GetDatabase("school")
		.GetCollection("students");
		
	foreach(var student in students.FindAllAs<BsonDocument>())
	{
		//student["scores"].Dump("all scores", 2);
		
		var minHomeworkScore = student["scores"]
			.AsBsonArray
			.Select((sc,i)  => new {Index = i, Doc = sc})
			.Where(a => a.Doc["type"].Equals("homework"))
			.MinBy(a => a.Doc["score"].AsDouble);
			
		student["scores"].AsBsonArray.RemoveAt(minHomeworkScore.Index);
		
		//student["scores"].Dump("min homework removed", 2);
		
		students.Save(student);
	}
	
	/* same number of documents
	and student 100 must have three scores: 
	db.students.find({_id:100}).pretty()
	*/
	System.Diagnostics.Debug.Assert(students.Count().Equals(200));
	
	BsonDocument hundredth = students.FindOneAs<BsonDocument>(Query.EQ("_id", 100));
	System.Diagnostics.Debug.Assert(hundredth["scores"].AsBsonArray.Count.Equals(3));
	
	/* To verify that you have completed this task correctly, provide the identify of the student with the highest average in the class with following query that uses the aggregation framework. The answer will appear in the _id field of the resulting document.

	db.students.aggregate(
		{'$unwind':'$scores'},
		{'$group':{'_id':'$_id', 'average':{$avg:'$scores.score'}}},
		{'$sort':{'average':-1}},
		{'$limit':1})
	*/
	AggregateResult result = students.Aggregate(
		new BsonDocument("$unwind", "$scores"),
		new BsonDocument("$group",
			new BsonDocument()
			{
				{"_id", "$_id"},
				{"average", new BsonDocument("$avg", "$scores.score")}
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