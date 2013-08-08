using MongoDB.Driver;
using Nancy;
using Nancy.ModelBinding;

namespace Dgg.tengen_M101J.BlogAssignment
{
	public class BlogController : NancyModule
	{
		public BlogController()
		{
			MongoDatabase db = new MongoClient("mongodb://localhost:27017")
				.GetServer()
				.GetDatabase("blog");

			var users = new UserDao(db);

			Get["/"] = _ => View["blog_template.html"];

			Get["/signup"] = _ => View["signup.html", new Models.Signup()];
			Post["/signup"] = _ =>
			{
				var signup = this.Bind<Models.Signup>();
				if (validateSignup(signup))
				{
					// good user
					if (!users.AddUser(signup.username, signup.password, signup.email))
					{
						signup.username_error = "Username already in use, Please choose another";
					}
					//return View["signup.html", signup];
				}
				return View["signup.html", signup];
			};
		}

		private bool validateSignup(Models.Signup signup)
		{
			return true;
		}
	}
}