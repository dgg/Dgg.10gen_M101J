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
MongoCollection collection = db.GetCollection("dotNotationTest");
collection.Drop();

var random = new Random();
for (int i = 0; i < 10; i++)
{
	collection.Insert(
		new BsonDocument("_id", i)
		{
			{"start", new BsonDocument{{"x", random.Next(90) + 10}, {"y", random.Next(90) + 10}}},
			{"end", new BsonDocument{{"x", random.Next(90) + 10}, {"y", random.Next(90) + 10}}},
		});
}

IMongoQuery query = Query.Null;
collection.FindAs<BsonDocument>(query)
	.Select(d => d.ToString())
	.Dump("findAll");

// {"start.x" : {$gt : 50}}
query = Query.GT("start.x", 50);
collection.FindAs<BsonDocument>(query)
	.Select(d => d.ToString())
	.Dump(query.ToString());
	
// {"start.y" : true, "_id" : false}
IMongoFields fields = Fields.Include("start.y").Exclude("_id");
collection.FindAs<BsonDocument>(query).SetFields(fields)
	.Select(d => d.ToString())
	.Dump(query.ToString() + " , " + fields.ToString());