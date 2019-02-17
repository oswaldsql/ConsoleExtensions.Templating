namespace ConsoleExtensions.Templating.Renderers
{
	using System;
	using System.Globalization;

	using ConsoleExtensions.Proxy;

	internal class ColorRender : Renderer
	{
		public ConsoleColor? Color
		{
			get
			{
				if (Enum.TryParse(this.Config, true, out ConsoleColor color))
				{
					return color;
				}

				return null;
			}
		}

		public override void Render(IConsoleProxy proxy, object arg, CultureInfo culture)
		{
			var consoleColor = this.Color;

			if (consoleColor == null)
			{
				foreach (var subRenderer in this.SubRenderes)
				{
					subRenderer.Render(proxy, arg, culture);
				}

				return;
			}

			proxy.GetStyle(out var original);
			proxy.Style(new ConsoleStyle("temp", consoleColor));
			foreach (var subRenderer in this.SubRenderes)
			{
				subRenderer.Render(proxy, arg, culture);
			}

			proxy.Style(original);
		}
	}
}