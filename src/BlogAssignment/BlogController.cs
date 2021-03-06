﻿using System;
using System.Text.RegularExpressions;
using Dgg.tengen_M101J.BlogAssignment.Models;
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
			var posts = new PostDao(db);

			Get["/"] = _ =>
			{
				string username = extractUsername(sessions, Request);

				var latestPosts = posts.FindByDateDescending(10);
				var model = new Home
				{
					username = username,
					myPosts = latestPosts
				};
				return View["blog_template.cshtml", model];
			};

			Get["/signup"] = _ => View["signup.html", new Signup()];
			Post["/signup"] = _ =>
			{
				var signup = this.Bind<Signup>();
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

			Get["/welcome"] = _ =>
			{
				string sessionId = extractSessionId(Request);
				string username = sessions.FindUsernameBySessionId(sessionId);

				// there is no session
				if (string.IsNullOrEmpty(sessionId))
				{
					return Response.AsRedirect("/signup");
				}
				return View["welcome.html", new { username }];
			};

			Get["/login"] = _ => View["login.html", new Login()];
			Post["/login"] = _ =>
			{
				var login = this.Bind<Login>();
				User user = users.ValidateUser(login.username, login.password);
				if (user != null)
				{
					string sessionId = sessions.StartSession(user.Id);
					if (string.IsNullOrEmpty(sessionId)) throw new Exception("catastrofic error");

					return Response.AsRedirect("/welcome").AddCookie("session", sessionId);
				}
				login.login_error = "invalid login";
				return View["login.html", login];
			};

			Get["/logout"] = _ =>
			{
				string sessionId = extractSessionId(Request);
				// there is no session
				if (string.IsNullOrEmpty(sessionId))
				{
					return Response.AsRedirect("/signup");
				}
				else
				{
					sessions.EndSession(sessionId);
					// removes cookie
					return Response.AsRedirect("/login").AddCookie("session", sessionId, DateTime.MinValue);
				}
			};

			Get["/posts/{permalink}"] = args =>
			{
				string permalink = args.permalink;

				Post post = posts.Get(permalink);
				if (post == null) return Response.AsRedirect("/post_not_found");

				string username = extractUsername(sessions, Request);
				var model = new Entry
				{
					username = username,
					post = post
				};
				return View["entry_template.cshtml", model];
			};

			Get["newpost"] = _ =>
			{
				string username = extractUsername(sessions, Request);
				if (string.IsNullOrEmpty(username)) return Response.AsRedirect("/login");
				var model = new NewPost
				{
					username = username
				};
				return View["new_post.html", model];
			};
			Post["newpost"] = _ =>
			{
				string username = extractUsername(sessions, Request);
				if (string.IsNullOrEmpty(username)) return Response.AsRedirect("/login");

				var model = this.Bind<NewPost>();
				if (string.IsNullOrEmpty(model.subject) || string.IsNullOrEmpty(model.body))
				{
					model.errors = "post must contain a title and blog entry.";
					model.username = username;

					return View["new_post.html", model];
				}
				else
				{
					string[] tags = (model.tags ?? string.Empty).Split(',');
					string permalink = posts.AddPost(model.subject, model.body.Replace("\n", "<br/>"), tags, username);

					return Response.AsRedirect("/posts/" + permalink);
				}
			};

			Post["/newcomment"] = _ =>
			{
				var model = this.Bind<NewComment>();

				Post post = posts.Get(model.permalink);
				if (post == null) return Response.AsRedirect("/post_not_found");

				if (string.IsNullOrEmpty(model.name) || string.IsNullOrEmpty(model.body))
				{
					model.errors = "Post must contain your name and an actual comment";
					var entry = new Entry { post = post, comment = model };
					return View["entry_template.cshtml", entry];
				}
				
				posts.AddComment(model.name, model.email, model.body, model.permalink);
				return Response.AsRedirect("/posts/" + model.permalink);
			};

			Get["post_not_found"] = _ => View["post_not_found.html"];

			Get["/tags/{tag}"] = args =>
			{
				string username = extractUsername(sessions, Request);
				var myPosts = posts.FindByTag(args.tag, 10);
				var model = new Home
				{
					username = username,
					myPosts = myPosts
				};
				return View["blog_template.cshtml", model];
			};
		}

		private static string extractUsername(SessionDao sessions, Request request)
		{
			string sessionId = extractSessionId(request);
			string username = sessions.FindUsernameBySessionId(sessionId);
			return username;
		}

		private static string extractSessionId(Request request)
		{
			// cookie gets encoded
			string sessionId;
			request.Cookies.TryGetValue("session", out sessionId);
			sessionId = System.Web.HttpUtility.UrlDecode(sessionId);
			return sessionId;
		}

		private bool validateSignup(Signup signup)
		{
			Regex userValidator = new Regex("^[a-zA-Z0-9_-]{3,20}$", RegexOptions.Compiled),
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