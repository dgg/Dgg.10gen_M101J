using Nancy;

namespace HelloWorld_Nancy
{
	public class Handlers : NancyModule
	{
		public Handlers()
		{
			Get["/"] = _ => View["Hello.html", new { Name = "Nancy's Super Simple View Engine" }];
		}
	}
}