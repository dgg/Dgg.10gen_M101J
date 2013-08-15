using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Dgg.tengen_M101J.BlogAssignment
{
	public class PostDao
	{
		private readonly MongoCollection<Post> _posts;

		public PostDao(MongoDatabase db)
		{
			_posts = db.GetCollection<Post>("posts");
		}

		public Post Get(string permalink)
		{
			return _posts.FindOne(Query<Post>.EQ(p => p.Permalink, permalink));
		}

		public IEnumerable<Post> FindByDateDescending(int limit)
		{
			return _posts.AsQueryable()
				.OrderByDescending(p => p.Date)
				.Take(limit);
		}

		public string AddPost(string title, string body, string[] tags, string username)
		{
			var post = new Post(title, username, body, tags);

			_posts.Save(post);

			return post.Permalink;
		}

		public void AddComment(string name, string email, string body, string permalink)
		{
			var toBeAdded = new Comment(name, body) {Email = email};

			_posts.Update(Query<Post>.EQ(p => p.Permalink, permalink),
				Update<Post>.Push(p => p.Comments, toBeAdded));
		}
	}
}