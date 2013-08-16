namespace Dgg.tengen_M101J.BlogAssignment.Models
{
	public class Entry : Authenticated
	{
		public Entry()
		{
			post = Post.Empty();
			comment =  new NewComment();
		}
		public Post post { get; set; }
		public NewComment comment { get; set; }
		public Comment[] Comments { get { return post.Comments; } }
	}
}