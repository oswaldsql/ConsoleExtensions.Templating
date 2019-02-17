namespace ConsoleExtensions.Templating
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;

	using ConsoleExtensions.Proxy;
	using ConsoleExtensions.Templating.Renderers;

	public class Template
	{
		public Template()
		{
			Styles = new Dictionary<string, ConsoleStyle>();
			SubTemplates = new Dictionary<Type, Template>();
		}

		public Renderer RenderTree { get; set; }

		public Dictionary<string, ConsoleStyle> Styles { get; internal set; }

		internal Dictionary<Type, Template> SubTemplates { get; set; }

		public void Render(IConsoleProxy proxy, object arg, CultureInfo culture = null)
		{
			this.RenderTree.Render(proxy, arg, culture);
		}
	}
}