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
MongoCollection collection = db.GetCollection("insertTest");

BsonDocument doc = new BsonDocument()
{
	{"x", 1}
};
doc.Dump("does not have an _id property", 2);
collection.Insert(doc);
doc.Dump("has an _id property", 2);

var doc2 = new BsonDocument()
{
	{"_id",  new BsonObjectId(ObjectId.GenerateNewId())},
	{"x", 2}
};
doc2.Dump("does have an _id property", 2);
collection.Insert(doc2);
doc2.Dump("maintains same _id property", 2);

// add multiple documents
BsonDocument doc3 = new BsonDocument { {"x", 3 } },
	doc4  = new BsonDocument{{"x", 4}};
collection.InsertBatch(new []{doc3, doc4});

// exception by duplicate key
try
{
	collection.Insert(doc2);
}
catch (WriteConcernException ex)
{
	ex.Dump();
}

/* 
Do you expect the second insert below to succeed? 

MongoCollection people = new MongoClient()
	.GetServer().GetDatabase("school")
	.GetCollection("people");
	var andrew = new BsonDocument
	{
		{"name", "Andrew Erlichson"},
		{"company", "10gen"}
	};
	
	try
	{
		andrew.Dump(2);
		people.Insert(andrew);
		andrew.Dump(2);
		andrew.Remove("_id");
		andrew.Dump(2);
		people.Insert(andrew);
	}
	catch (Exception ex)
	{
		ex.Dump();
	}
*/
// Yes, because the removeField call will remove the _id key added by the driver in the first insert