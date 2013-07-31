using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Nancy;

namespace Hw_1._4
{
	public class Handlers : NancyModule
	{
		public Handlers()
		{
			Get["/"] = _ =>
			{
				var client = new MongoClient("mongodb://localhost:27017");
				MongoCollection collection = client
					.GetServer().GetDatabase("week1")
					.GetCollection("funnynumbers");

				AggregateResult result = collection.Aggregate(
					new BsonDocument
					{{
						 "$group", new BsonDocument
						 {
							{"_id", "$value"},
							{"count", new BsonDocument { {"$sum", 1} }}
						 }
					}},
					new BsonDocument
					{{
						 "$match", new BsonDocument
						 {
							{"count", new BsonDocument{{"$lte", 2}}}
						 }
					}},
					new BsonDocument{{"$sort", new BsonDocument{{"_id", 1}}}});

				double answer = result.ResultDocuments
					.Sum(d => d["_id"].AsDouble);


				return View["Answer.html", answer];
			};
		}
	}
}