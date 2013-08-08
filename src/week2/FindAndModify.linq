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
	MongoClient client = new MongoClient();
	MongoDatabase db = client.GetServer().GetDatabase("course");
	MongoCollection collection = db.GetCollection("findModifyTest");
	collection.Drop();
	
	const string counterId = "abc";
	int first;
	int numNeeded;
	
	numNeeded = 2;
	first = getRange(counterId, numNeeded, collection);
	string.Format("Range: {0} - {1}", first, first + numNeeded -1).Dump();
	
	numNeeded = 3;
	first = getRange(counterId, numNeeded, collection);
	string.Format("Range: {0} - {1}", first, first + numNeeded -1).Dump();
	
	numNeeded = 10;
	first = getRange(counterId, numNeeded, collection);
	string.Format("Range: {0} - {1}", first, first + numNeeded -1).Dump();
	
	collection.FindAllAs<BsonDocument>().Dump(2);
}

// provides unique ranges of numbers for a given id
private int getRange(string counterId, int numbersNeeded, MongoCollection collectionToStoreCounters)
{
	
	FindAndModifyResult result = collectionToStoreCounters.FindAndModify(	
		Query.EQ("_id", counterId),
		SortBy.Null,
		Update.Inc("counter", numbersNeeded),
		returnNew: true,
		upsert: true)
		.Dump(2);
		
	return result.ModifiedDocument["counter"].AsInt32 - numbersNeeded  +1;
}