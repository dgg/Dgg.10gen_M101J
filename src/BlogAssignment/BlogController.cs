using System;
using System.Text.RegularExpressions;
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
			var sessions = new SessionDao(db);

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
					else
					{
						string sessionId = sessions.StartSession(signup.username);

						return Response.AsRedirect("/welcome").AddCookie("session", sessionId);
					}
				}
				return View["signup.html", signup];
			};

			Get["/welcome"] = _ => View["welcome.html", new {username = "yo!"}];
		}

		private bool validateSignup(Models.Signup signup)
		{
			Regex userValidator =  new Regex("^[a-zA-Z0-9_-]{3,20}$", RegexOptions.Compiled),
				passwordValidator = new Regex("^.{3,20}$", RegexOptions.Compiled),
				emailValidator = new Regex("^[\\S]+@[\\S]+\\.[\\S]+$", RegexOptions.Compiled);
			bool valid = false;
			if (!userValidator.IsMatch(signup.username ?? string.Empty))
			{
				signup.username_error = "invalid username. try just letters and numbers";
			}
			else if (!passwordValidator.IsMatch(signup.password ?? string.Empty))
			{
				signup.password_error = "invalid password";
			}
			else if (!StringComparer.Ordinal.Equals(signup.password, signup.verify))
			{
				signup.verify_error = "password must match";
			}
			else if (!string.IsNullOrEmpty(signup.email) && !emailValidator.IsMatch(signup.email))
			{
				signup.email_error = "invalid email address";
			}
			else
			{
				valid = true;
			}
			return valid;
		}
	}
}