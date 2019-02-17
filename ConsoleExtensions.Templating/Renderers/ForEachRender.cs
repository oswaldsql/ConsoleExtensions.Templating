namespace ConsoleExtensions.Templating.Renderers
{
	using System.Collections;
	using System.Globalization;
	using System.Reflection;

	using ConsoleExtensions.Proxy;

	internal class ForEachRender : Renderer
	{
		public override void Render(IConsoleProxy proxy, object arg, CultureInfo culture)
		{
			if (arg == null)
			{
				return;
			}

			object value;
			if (string.IsNullOrEmpty(this.Config))
			{
				value = arg;
			}
			else
			{
				var property = arg.GetType().GetRuntimeProperty(this.Config);
				value = property?.GetValue(arg);
			}

			if (!(value is IEnumerable enumerable))
			{
				foreach (var subRenderer in this.SubRenderes)
				{
					subRenderer.Render(proxy, value, culture);
				}
			}
			else
			{
				foreach (var o in enumerable)
				{
					foreach (var subRenderer in this.SubRenderes)
					{
						subRenderer.Render(proxy, o, culture);
					}
				}
			}
		}
	}
}