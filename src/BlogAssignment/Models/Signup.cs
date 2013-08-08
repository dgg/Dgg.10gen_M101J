namespace Dgg.tengen_M101J.BlogAssignment.Models
{
	public class Signup
	{
		public string email { get; set; }
		public string username { get; set; }
		public string password { get; set; }
		public string verify { get; set; }

		public string username_error { get; set; }
		public string password_error { get; set; }
		public string verify_error { get; set; }
		public string email_error { get; set; }
	}
}