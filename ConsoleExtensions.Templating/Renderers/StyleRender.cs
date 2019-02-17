namespace ConsoleExtensions.Templating.Renderers
{
	using System.Globalization;

	using ConsoleExtensions.Proxy;

	internal class StyleRender : Renderer
	{
		public override void Render(IConsoleProxy proxy, object arg, CultureInfo culture)
		{
			if (this.Template.Styles.TryGetValue(this.Config, out ConsoleStyle style))
			{
				proxy.GetStyle(out var original);
				proxy.Style(style);
				foreach (var subRenderer in this.SubRenderes)
				{
					subRenderer.Render(proxy, arg, culture);
				}

				proxy.Style(original);
			}
			else
			{
				foreach (var subRenderer in this.SubRenderes)
				{
					subRenderer.Render(proxy, arg, culture);
				}
			}
		}
	}
}