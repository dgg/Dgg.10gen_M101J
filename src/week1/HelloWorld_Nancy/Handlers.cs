using MongoDB.Bson;
using MongoDB.Driver;
using Nancy;

namespace HelloWorld_Nancy
{
	public class Handlers : NancyModule
	{
		public Handlers()
		{
			Get["/"] = _ =>
			{
				var client = new MongoClient("mongodb://localhost:27017");
				MongoCollection<BsonDocument> collection = client
					.GetServer()
					.GetDatabase("course")
					.GetCollection("hello");

				BsonDocument document = collection.FindOne();
				// mongo does not support dynamic, we resort to map to anonymous objects ourselves
				return View["Hello.html", new { Name = document["Name"].AsString }];
			};
		}
	}
}