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
			_posts.EnsureIndex(IndexKeys<Post>.Hashed(p => p.Permalink));

			Post found = _posts.FindOne(Query<Post>.EQ(p => p.Permalink, permalink));
			return found;
		}

		public IEnumerable<Post> FindByDateDescending(int limit)
		{
			_posts.EnsureIndex(IndexKeys<Post>.Descending(p => p.Date));

			IQueryable<Post> explainable = _posts.AsQueryable()
				.OrderByDescending(p => p.Date)
				.Take(limit);
			
			return explainable;
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

		public IEnumerable<Post> FindByTag(string tag, int limit)
		{
			_posts.EnsureIndex(IndexKeys<Post>.Descending(p => p.Date));
			_posts.EnsureIndex(IndexKeys<Post>.Ascending(p => p.Tags));

			var explainable = _posts.AsQueryable()
				.Where(p => p.Tags.Contains(tag))
				.OrderByDescending(p => p.Date)
				.Take(limit);

			return explainable;
		}

		public void Like(string permalink, int commentIndex)
		{
			_posts.Update(Query<Post>.EQ(p => p.Permalink, permalink),
				Update<Post>.Inc(p => p.Comments[commentIndex].num_likes, 1));
		}
	}
}