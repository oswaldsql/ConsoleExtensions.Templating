namespace ConsoleExtensions.Templating.Renderers
{
	using System;
	using System.Globalization;

	using ConsoleExtensions.Proxy;

	internal class ClearLineRender : Renderer
	{
		public override void Render(IConsoleProxy proxy, object arg, CultureInfo culture)
		{
			proxy.GetPosition(out var consoleProxy);
			var count = Math.Max(proxy.WindowWidth - consoleProxy.Left, 0);
			proxy.Write(new string(' ', count));
		}
	}
}