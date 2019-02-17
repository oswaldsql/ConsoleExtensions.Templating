namespace ConsoleExtensions.Templating.Renderers
{
	using System.Globalization;

	using ConsoleExtensions.Proxy;

	internal class HrRender : Renderer
	{
		public override void Render(IConsoleProxy proxy, object arg, CultureInfo culture)
		{
			proxy.GetPosition(out var consoleProxy);
			if (consoleProxy.Left != 0)
			{
				proxy.WriteLine();
			}

			proxy.Write(new string('-', proxy.WindowWidth));
		}
	}
}