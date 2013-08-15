using System;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dgg.tengen_M101J.BlogAssignment
{
	public class Post
	{
		public Post(string title, string author, string body, string[] tags)
		{
			Title = title;
			Author = author;
			Body = body;
			Tags = tags;

			Permalink = calculatePermaLink(title);
			Comments = new Comment[0];
			Date = DateTime.UtcNow;
		}

		public ObjectId Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string Body { get; set; }
		public string Permalink { get; private set; }
		public string[] Tags { get; set; }
		public Comment[] Comments { get; set; }
		public DateTime Date { get; set; }

		private static string calculatePermaLink(string title)
		{
			string permalink = Regex.Replace(title, "\\s", "_", RegexOptions.Compiled);
			permalink = Regex.Replace(permalink, "\\W", string.Empty, RegexOptions.Compiled);
			permalink = permalink.ToLower();
			return permalink;
		}

		[BsonIgnore]
		public string AllTags
		{
			get { return string.Join(", ", Tags); }
		}
	}
}