using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
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
				return View["Hello.html", document.AsDinamicDictionary()];
			};
		}
	}
}