namespace ConsoleExtensions.Templating.Renderers
{
	using System.Globalization;

	using ConsoleExtensions.Proxy;

	internal class EndRenderer : Renderer
	{
		public EndRenderer(string substring)
		{
			this.Substring = substring;
		}

		public string Substring { get; }

		public override void Render(IConsoleProxy proxy, object arg, CultureInfo culture)
		{
		}
	}
}