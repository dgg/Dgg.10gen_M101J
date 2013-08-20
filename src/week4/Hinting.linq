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

MongoCollection foo = new MongoClient()
	.GetServer().GetDatabase("test")
	.GetCollection("foo");
	
//foo.Drop();
//for(int i = 0; i < 10000; i++) foo.Insert(new {a = i, b = i, c=i}.ToBsonDocument());

foo.EnsureIndex(IndexKeys.Ascending("a"));
foo.EnsureIndex(IndexKeys.Ascending("b"));
foo.EnsureIndex(IndexKeys.Ascending("c"));

IMongoQuery query = Query.And(
	Query.EQ("a", 40000),
	Query.EQ("b", 40000),
	Query.EQ("c", 40000));
	
var explanation = foo.FindAs<BsonDocument>(query)
	.SetHint(new BsonDocument("c", 1))
	.Explain();
	
explanation.Dump(2);