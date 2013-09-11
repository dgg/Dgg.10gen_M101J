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
MongoDatabase db = client.GetServer().GetDatabase("final_7");
MongoCollection images = db.GetCollection("images");
MongoCollection albums = db.GetCollection("albums");

IMongoQuery sunrises = Query.EQ("tags", "sunrises");
long sunrisesCount = images.Count(sunrises);

Debug.Assert(sunrisesCount ==  50054, "fifty thousand-ish images tagged 'sunrises'");

long imageCount = images.Count().Dump("Image count");
long orphanCount = 0;

albums.EnsureIndex("images");

foreach(var image in images.FindAllAs<BsonDocument>())
{
	int imageId = image["_id"].AsInt32;
	BsonDocument album = albums.FindOneAs<BsonDocument>(Query.EQ("images", imageId));
	if (album == null)
	{
		orphanCount++;
		images.Remove(Query.EQ("_id", imageId));
	}
}

orphanCount.Dump("number of orphan images");

Debug.Assert(images.Count() == 90017, "Image count");

images.Count(sunrises).Dump("sunrises after orphan removal");