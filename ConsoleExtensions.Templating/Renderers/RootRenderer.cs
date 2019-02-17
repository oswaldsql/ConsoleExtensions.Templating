namespace ConsoleExtensions.Templating.Renderers
{
	using System.Globalization;

	using ConsoleExtensions.Proxy;

	internal class RootRenderer : Renderer
	{
		private readonly Renderer[] renderers;

		public RootRenderer(Renderer[] renderers)
		{
			this.renderers = renderers;
		}

		public override void Render(IConsoleProxy proxy, object arg, CultureInfo culture)
		{
			foreach (var renderer in this.renderers)
			{
				renderer.Render(proxy, arg, culture);
			}
		}
	}
}