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
	/*MongoClient client = new MongoClient();
	MongoDatabase db = client.GetServer().GetDatabase("course");
	MongoCollection collection = db.GetCollection("dotNotationTest");
	collection.Drop();
	
	*/
	long thirtySixMB = 37748736;
	withDummyFile(
		"d:\\temp\\dummy.txt",
		thirtySixMB,
		fi =>
		{
			MongoClient client = new MongoClient();
			MongoDatabase db = client.GetServer().GetDatabase("course");
					
			var videos = new MongoGridFS(db);
								
			MongoGridFSFileInfo video = videos.Upload(fi.FullName);
			BsonDocument meta = new BsonDocument("description", "big empty file")
			{
				{"tags", new BsonArray(new[]{"empty", "big"})}
			};
			videos.SetMetadata(video, meta);
		
			video.Id.AsObjectId.Dump("ID in Files (fs.files) collection", 2);
			
			"Let's read it back".Dump();
			
			MongoGridFSFileInfo found = videos.FindOne(Query.EQ("filename", fi.FullName));
			using (MongoGridFSStream stream = found.OpenRead())
			{
				System.Diagnostics.Debug.Assert(stream.Length.Dump("GridFs file size").Equals(thirtySixMB));
			}
			
			videos.FindOne(Query.Null).Dump(1);
			videos.Chunks.Count().Dump("Chunks count");
			
			videos.Delete(Query.Null);
		});
}

// Define other methods and classes here

public void withDummyFile(string fileName, long length, Action<FileInfo> @do)
{
	using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
	{
		stream.SetLength(length);
	}
	FileInfo fi = new FileInfo(fileName);
	@do(fi);
	fi.Delete();
}
