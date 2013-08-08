using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dgg.tengen_M101J.BlogAssignment
{
	public class UserDao
	{
		private readonly MongoCollection<BsonDocument> _users;
		private readonly Random _random;

		public UserDao(MongoDatabase db)
		{
			_users = db.GetCollection("users");
			_random = new Random();
		}

		public bool AddUser(string username, string password, string email)
		{
			string hashedPassword = hash(password, _random.Next().ToString(CultureInfo.InvariantCulture));

			// XXX WORK HERE
			// create an object suitable for insertion into the user collection
			// be sure to add username and hashed password to the document. problem instructions
			// will tell you the schema that the documents must follow.
			var user = new User(username, hashedPassword);

			if (!string.IsNullOrEmpty(email))
			{
				// XXX WORK HERE
				// if there is an email address specified, add it to the document too.
				user.Email = email;
			}

			try
			{
				// XXX WORK HERE
				// insert the document into the user collection here
				_users.Insert(user);
				return true;
			}
			catch (MongoException)
			{
				Console.Error.Write("Username already in use: {0}", username);
				return false;
			}
		}

		private string hash(string password, string salt)
		{
			/*String saltedAndHashed = password + "," + salt;
			MessageDigest digest = MessageDigest.getInstance("MD5");
			digest.update(saltedAndHashed.getBytes());
			BASE64Encoder encoder = new BASE64Encoder();
			byte hashedBytes[] = (new String(digest.digest(), "UTF-8")).getBytes();
			return encoder.encode(hashedBytes) + "," + salt;*/

			string saltedAndHashed = password + "," + salt;
			MD5 md5 = MD5.Create();
			byte[] digest = md5.ComputeHash(Encoding.UTF8.GetBytes(saltedAndHashed));

			return Convert.ToBase64String(digest) + "," + salt;
		}
	}
}