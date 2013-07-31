using MongoDB.Bson;
using Nancy;

namespace HelloWorld_Nancy
{
	public static class BsonExtensions
	{
		public static DynamicDictionary AsDinamicDictionary(this BsonDocument document)
		{
			var dic = new DynamicDictionary();
			foreach (var element in document.Elements)
			{
				dic.Add(element.Name, element.Value);
			}
			return dic;
		}
	}
}