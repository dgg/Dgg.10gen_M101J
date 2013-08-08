namespace Dgg.tengen_M101J.BlogAssignment
{
	public class User
	{
		public User(string id, string password)
		{
			Id = id;
			Password = password;
		}

		public string Id { get; private set; }
		public string Password { get; private set; }
		public string Email { get; set; }
	}
}