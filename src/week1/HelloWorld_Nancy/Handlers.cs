using Nancy;

namespace HelloWorld_Nancy
{
	public class Handlers : NancyModule
	{
		public Handlers()
		{
			Get["/"] = _ => "Hello from Nancy";
		}
	}
}