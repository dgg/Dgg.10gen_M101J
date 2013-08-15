using System.Collections.Generic;

namespace Dgg.tengen_M101J.BlogAssignment.Models
{
	public class Home
	{
		public bool isLoggedIn { get; set; }
		public string username { get; set; }
		public IEnumerable<Post> myPosts { get; set; }
	}
}