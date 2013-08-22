using Nancy.ViewEngines.Razor;

namespace Dgg.tengen_M101J.BlogAssignment.Models
{
	public static class HtmlHelperExtensions
	{
		public static int SafeLength<T>(this T[] array)
		{
			return array != null ? array.Length : 0;
		}
	}
}