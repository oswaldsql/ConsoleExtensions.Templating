namespace ConsoleExtensions.Templating
{
	using System.Globalization;

	using ConsoleExtensions.Proxy;

	public static class TemplatingExtension
	{
		public static IConsoleProxy WriteTemplate(this IConsoleProxy proxy, string template, object arg = null, CultureInfo culture = null)
		{
			var parsed = TemplateParser.Default.Parse(template);
			parsed.Render(proxy, arg, culture);
			return proxy;
		}

		public static IConsoleProxy WriteTemplate(this IConsoleProxy proxy, Template template, object arg = null, CultureInfo culture = null)
		{
			template.Render(proxy, arg, culture);

			return proxy;
		}
	}
}