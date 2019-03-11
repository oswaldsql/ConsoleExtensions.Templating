namespace ConsoleExtensions.Templating
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;

	using ConsoleExtensions.Proxy;
	using ConsoleExtensions.Templating.Renderers;

	public class Template
	{
		private readonly TemplateParser parser;

		internal Template(TemplateParser parser)
		{
			this.parser = parser;
		}

		public Renderer RenderTree { get; set; }

		public Dictionary<string, ConsoleStyle> Styles => this.parser.Style;

		internal Dictionary<Type, Template> TypeTemplates => this.parser.TypeTemplates;

		internal Dictionary<Type, Func<object, object>> TypeConverters => this.parser.TypeConverters;

		public void Render(IConsoleProxy proxy, object arg, CultureInfo culture = null)
		{
			this.RenderTree.Render(proxy, arg, culture);
		}
	}
}