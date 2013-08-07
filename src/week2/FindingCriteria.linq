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
MongoCollection collection = db.GetCollection("findCriteriaTest");
collection.Drop();

var random = new Random();
for (int i = 0; i < 10; i++)
{
	collection.Insert(new BsonDocument("x", random.Next(2)){{"y", random.Next(100)}});
}

collection.Count().Dump("count");
collection.FindAllAs<BsonDocument>().Dump("findAll", 2);

// simple queries
IMongoQuery query = Query.EQ("x", 0);
query.ToString().Dump();
collection.Count(query).Dump(query.ToString());
collection.FindAs<BsonDocument>(query).Dump(query.ToString(), 2);

// compound queries
query = Query.And(query,
	Query.GT("y", 10),
	Query.LT("y", 90));
collection.Count(query).Dump(query.ToString());
collection.FindAs<BsonDocument>(query).Dump(query.ToString(), 2);