namespace ConsoleExtensions.Templating.Renderers
{
	using System.Globalization;

	using ConsoleExtensions.Proxy;

	internal class TextRenderer : Renderer
	{
		private readonly string value;

		public TextRenderer(string value)
		{
			this.value = value;
		}

		public override void Render(IConsoleProxy proxy, object arg, CultureInfo culture)
		{
			proxy.Write(this.value);
		}
	}
}