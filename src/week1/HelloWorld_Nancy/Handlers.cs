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

			Get["/fruits"] = _ =>
			{
				var fruits = new[] { "apple", "orange", "banana", "peach" };

				return View["Fruits.html", fruits];
			};

			Post["/favorite-fruit"] = _ =>
			{
				DynamicDictionaryValue fruit = Request.Form.fruit;
				string message = fruit.HasValue ? 
					string.Format("your favorite fruits is {0}", fruit.Value) :
					"Please, select a fruit";
				return View["FavoriteFruit.html", message];
			};
		}
	}
}