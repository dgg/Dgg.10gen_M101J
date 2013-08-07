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
MongoCollection collection = db.GetCollection("findTest");

for (int i = 0; i < 10; i++)
{
	collection.Insert(new BsonDocument("x", i));
}

BsonDocument one = collection.FindOneAs<BsonDocument>()
	.Dump("findOne", 2);

MongoCursor cursor = collection.FindAllAs<BsonDocument>()
	.Dump("findAll", 2);
/*foreach (BsonDocument element in cursor)
{
	element.Dump(2);
}*/

collection.Count()
	.Dump("findOne");
	
/*
In the following code snippet:

    MongoClient client = new MongoClient();
    DB db = client.getDB("school");
    DBCollection people = db.getCollection("people");
    DBObject doc;
    xxxx
    System.out.println(doc);
	
	MongoCollection people = new MongoClient().GetServer().GetDatabase("school")
		.GetCollection("people");
	BsonDocument doc;
	zzzz
	doc.Dump();

Please enter the simplest one line of Java code that would be needed in place of xxxx to make it print one document from the people collection.
*/
// doc = people.findOne();
// doc = people.FindOneAs<BsonDocument>();