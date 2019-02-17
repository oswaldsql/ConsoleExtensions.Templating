namespace ConsoleExtensions.Templating.Renderers
{
	using System.Globalization;
	using System.Reflection;

	using ConsoleExtensions.Proxy;

	public abstract class Renderer
	{
		public bool IsClosed { get; set; }

		internal virtual string Config { get; set; }

		internal Renderer[] SubRenderes { get; set; }

		internal Template Template { get; set; }

		public abstract void Render(IConsoleProxy proxy, object arg, CultureInfo culture);

		internal object GetValueFromPropertyString(object arg, string propertyString)
		{
			if (propertyString == string.Empty)
			{
				return arg;
			}

			var properties = propertyString.Split('.');
			foreach (var propertyName in properties)
			{
				var property = arg?.GetType().GetRuntimeProperty(propertyName);
				if (property == null)
				{
					return null;
				}

				arg = property.GetValue(arg);
				if (arg == null)
				{
					return null;
				}
			}

			return arg;
		}
	}
}