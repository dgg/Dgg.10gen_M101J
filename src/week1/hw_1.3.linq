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
MongoDatabase db = client.GetServer().GetDatabase("week1");
MongoCollection collection = db.GetCollection("funnynumbers");

AggregateResult result = collection.Aggregate(
	new BsonDocument().Add(
		"$group", new BsonDocument()
			.Add("_id", "$value")
			.Add("count", new BsonDocument{{"$sum", 1}})),
	new BsonDocument().Add(
		"$match", new BsonDocument()
			.Add("count", new BsonDocument{{"$gt", 2}})),
	new BsonDocument().Add(
		"$sort", new BsonDocument{{"_id", 1}}));
		
result.ResultDocuments
	.Sum(d => d["_id"].AsDouble)
	.Dump();