namespace Dgg.tengen_M101J.BlogAssignment
{
	public class Comment
	{
		public string Name { get; private set; }
		public string Body { get; private set; }

		public Comment(string name, string body)
		{
			Name = name;
			Body = body;
		}

		public string Email { get; set; }
	}
}