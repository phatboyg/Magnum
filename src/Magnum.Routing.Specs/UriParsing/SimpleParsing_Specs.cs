namespace Magnum.Routing.Specs.UriParsing
{
	using System;
	using TestFramework;


	[Scenario]
	public class SimpleParsing_Specs
	{
		Uri _base = new Uri("http://localhost");

		[Then]
		public void Should_parse_query_string()
		{
			var builder = new Uri(_base, "version");

			builder.AbsolutePath.ShouldEqual("/version");
		}
	}
}