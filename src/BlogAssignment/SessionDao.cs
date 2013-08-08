using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Dgg.tengen_M101J.BlogAssignment
{
	public class SessionDao
	{
		private readonly MongoCollection<BsonDocument> _sessions;

		public SessionDao(MongoDatabase db)
		{
			_sessions = db.GetCollection("sessions");
		}

		public string FindUsernameBySessionId(string sessionId)
		{
			BsonDocument session = _sessions.FindOneById(sessionId);
			return session == null ? null : session["username"].AsString;
		}

		public string StartSession(string username)
		{
			var random = new Random();
			var randomBytes = new byte[32];
			random.NextBytes(randomBytes);

			string sessionId = Convert.ToBase64String(randomBytes);
			var session = new BsonDocument("_id", sessionId)
			{
				{ "username", username }
			};

			_sessions.Insert(session);

			return session["_id"].AsString;
		}

		public void EndSession(string sessionId)
		{
			_sessions.Remove(Query.EQ("_id", sessionId));
		}
	}
}