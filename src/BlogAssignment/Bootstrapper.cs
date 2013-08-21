using MongoDB.Bson.Serialization.Conventions;
using Nancy;

namespace Dgg.tengen_M101J.BlogAssignment
{
	public class Bootstrapper : DefaultNancyBootstrapper
	{
		protected override void ConfigureConventions(Nancy.Conventions.NancyConventions nancyConventions)
		{
			base.ConfigureConventions(nancyConventions);

			var camelPack = new ConventionPack {new CamelCaseElementNameConvention()};
			ConventionRegistry.Register("camelCase", camelPack, _ => true);
		}
	}
}