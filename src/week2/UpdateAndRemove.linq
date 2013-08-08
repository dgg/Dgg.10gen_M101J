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
MongoCollection collection = db.GetCollection("updaterTest");
collection.Drop();

foreach (var name in new[]{"alice", "bobby", "cathy", "david", "ethan"})
{
	collection.Insert(new BsonDocument("_id", name));
}

collection.FindAllAs<BsonDocument>()
	.Dump("findAll", 2);
	
// wholesale
IMongoQuery query = Query.EQ("_id", "alice");
collection.Update(query, Update.Replace(new BsonDocument("age", 24)));
collection.FindAllAs<BsonDocument>()
	.Dump("wholesale age", 2);
collection.Update(query, Update.Replace(new BsonDocument("gender", "F")));
collection.FindAllAs<BsonDocument>()
	.Dump("wholesale gender", 2);
	
// $set
collection.Update(query, Update.Set("age", 24));
collection.FindAllAs<BsonDocument>()
	.Dump("set age", 2);
	
//upsert
collection.Update(
	Query.EQ("_id", "frank"),
	Update.Set("gender", "M"));
collection.FindAllAs<BsonDocument>()
	.Dump("no upsert", 2);
collection.Update(
	Query.EQ("_id", "frank"),
	Update.Set("gender", "M"),
	UpdateFlags.Upsert);
collection.FindAllAs<BsonDocument>()
	.Dump("upserted male frank", 2);
	
// multi-update
collection.Update(
	Query.Null,
	Update.Set("title", "Dr."),
	UpdateFlags.Multi);
collection.FindAllAs<BsonDocument>()
	.Dump("everyone is a Dr.", 2);
	
// remove
collection.Remove(Query.EQ("_id", "alice"));
collection.FindAllAs<BsonDocument>()
	.Dump("alice is evicted", 2);
	
/*
In the following code fragment, what is the Java expression in place of xxxx that will set the field "examiner" to the value "Jones" for the document with _id of 1. Please use the $set operator.

	# update using $set
	scores.update(new BasicDBObject("_id", 1), xxxx);

	---
		
	scores.Update(Query.EQ("_id", 1), xxxx);
*/
// new BasicDBObject("$set", new BasicDBObject("examiner", "Jones"))
// Update.Set("examiner", "Jones")