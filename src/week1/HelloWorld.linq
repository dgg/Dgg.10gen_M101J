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
	addDocument(clear : true);
	queryDocument();
}

// Define other methods and classes here

private static void addDocument(bool clear = true)
{
	var server = new MongoServer(
		new MongoServerSettings
		{
			Server = new MongoServerAddress("localhost"),
		});
	var settings = new MongoDatabaseSettings()
	{
		WriteConcern = WriteConcern.Acknowledged
	};
	var db = new MongoDatabase(server, "course", settings);

	MongoCollection collection = db.GetCollection("hello");
	
	if (clear)
	{
		collection.Drop();
	}
	collection.Save(new {Name = "Mongo"}.ToBsonDocument());
}

private static void queryDocument()
{
	var client = new MongoClient(new MongoUrl("mongodb://localhost:27017"));
	MongoCollection collection = client
		.GetServer()
		.GetDatabase("course")
		.GetCollection("hello");
		
	collection.FindAllAs<BsonDocument>().Dump(2);
}
