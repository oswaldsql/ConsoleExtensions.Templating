namespace ConsoleExtensions.Templating.Renderers
{
	using System.Globalization;

	using ConsoleExtensions.Proxy;

	internal class LineBreakRender : Renderer
	{
		public override void Render(IConsoleProxy proxy, object arg, CultureInfo culture)
		{
			proxy.WriteLine();
		}
	}
}