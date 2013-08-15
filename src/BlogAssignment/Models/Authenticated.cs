namespace Dgg.tengen_M101J.BlogAssignment.Models
{
	public abstract class Authenticated
	{
		public bool isLoggedIn { get; private set; }

		private string _username;
		public string username
		{
			get { return _username; }
			set { 
				_username = value;
				isLoggedIn = !string.IsNullOrEmpty(value);
			}
		}
	}
}