using System.Collections.Generic;

namespace Dgg.tengen_M101J.BlogAssignment.Models
{
	public class Home : Authenticated
	{
		public IEnumerable<Post> myPosts { get; set; }
	}
}