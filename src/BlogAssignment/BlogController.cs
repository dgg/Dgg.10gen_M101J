using Nancy;

namespace Dgg.tengen_M101J.BlogAssignment
{
	public class BlogController : NancyModule
	{
		public BlogController()
		{
			Get["/"] = _ => View["blog_template.html", new { }];
		}
	}
}