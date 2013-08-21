namespace Dgg.tengen_M101J.BlogAssignment
{
	public class Comment
	{
		public string Author { get; private set; }
		public string Body { get; private set; }

		public Comment(string name, string author)
		{
			Author = name;
			Body = author;
		}

		public string Email { get; set; }
		public int num_likes { get; set; }
	}
}