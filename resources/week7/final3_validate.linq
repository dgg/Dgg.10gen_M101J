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

var client = new MongoClient("mongodb://localhost:27017");
MongoDatabase db = client.GetServer().GetDatabase("enron");
MongoCollection messages = db.GetCollection("messages");

string sentinal = "mrpotatohead@10gen.com";
string msg_id = "<8147308.1075851042335.JavaMail.evans@thyme>";

IMongoQuery query = Query.EQ("headers.To", sentinal);
long count = messages.Count(query);

Debug.Assert(count == 1, "there must be only one document");

BsonDocument doc =  messages.FindOneAs<BsonDocument>(query);

Debug.Assert(doc.Contains("headers"), "doc structure is not correct");
Debug.Assert(doc["headers"].AsBsonDocument.Contains("Message-ID"), "doc structure is not correct");
Debug.Assert(doc["headers"].AsBsonDocument["Message-ID"].AsString.Equals(msg_id, StringComparison.Ordinal), "Incorrect Message-ID");

 "Tests Passed for Final 3. Your Final 3 validation code is 897h6723ghf25gd87gh28".Dump();

