<Query Kind="Statements">
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

MongoClient client = new MongoClient();
MongoDatabase db = client.GetServer().GetDatabase("course");
MongoCollection collection = db.GetCollection("sortSkipLimitTest");
collection.Drop();

var random = new Random();
for (int i = 0; i < 10; i++)
{
	collection.Insert(
		new BsonDocument("_id", i)
		{
			{"start", new BsonDocument{{"x", random.Next(2)}, {"y", random.Next(90) + 10}}},
			{"end", new BsonDocument{{"x", random.Next(2)}, {"y", random.Next(90) + 10}}},
		});
}

collection.FindAllAs<BsonDocument>()
	.Select(d => d.ToString())
	.Dump("findAll");

// .sort({_id : -1})
IMongoSortBy sort = SortBy.Descending("_id");
MongoCursor cursor = collection.FindAllAs<BsonDocument>()
	.SetSortOrder(sort);
	
cursor.OfType<BsonDocument>()
	.Select(d => d.ToString())
	.Dump(cursor.Options.ToString());

// .skip(2)
cursor = collection.FindAllAs<BsonDocument>()
	.SetSortOrder(sort)
	.SetSkip(2);
	
cursor.OfType<BsonDocument>()
	.Select(d => d.ToString())
	.Dump(cursor.Options.ToString() + " | "  + cursor.Skip);
	
// .limit(3)
cursor = collection.FindAllAs<BsonDocument>()
	.SetSortOrder(sort)
	.SetSkip(2)
	.SetLimit(3);
	
cursor.OfType<BsonDocument>()
	.Select(d => d.ToString())
	.Dump(cursor.Options.ToString() + " | "  + cursor.Skip + " | " + cursor.Limit);
	
	
// .sort({"start.y" : -1})
sort = SortBy.Descending("start.y");
cursor = collection.FindAllAs<BsonDocument>()
	.SetSortOrder(sort);
	
cursor.OfType<BsonDocument>()
	.Select(d => d.ToString())
	.Dump(cursor.Options.ToString());
	
// .sort({"start.x" : 1, "start.y" : -1})
sort = SortBy.Ascending("start.x").Descending("start.y");
cursor = collection.FindAllAs<BsonDocument>()
	.SetSortOrder(sort);
	
cursor.OfType<BsonDocument>()
	.Select(d => d.ToString())
	.Dump(cursor.Options.ToString());