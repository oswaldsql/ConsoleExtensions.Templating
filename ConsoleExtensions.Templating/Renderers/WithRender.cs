namespace ConsoleExtensions.Templating.Renderers
{
	using System.Globalization;
	using System.Reflection;

	using ConsoleExtensions.Proxy;

	internal class WithRender : Renderer
	{
		public override void Render(IConsoleProxy proxy, object arg, CultureInfo culture)
		{
			object value;
			if (string.IsNullOrEmpty(this.Config))
			{
				value = arg;
			}
			else
			{
				var property = arg?.GetType().GetRuntimeProperty(this.Config);
				value = property?.GetValue(arg);
			}

			if (value != null)
			{
				foreach (var subRenderer in this.SubRenderes)
				{
					subRenderer.Render(proxy, value, culture);
				}
			}
		}
	}
}