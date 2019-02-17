namespace ConsoleExtensions.Templating.Renderers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using ConsoleExtensions.Templating.Token;

	internal class CommandFactory
	{
		public readonly Dictionary<string, Func<Renderer>> Commands = new Dictionary<string, Func<Renderer>>(StringComparer.OrdinalIgnoreCase)
			                                                              {
				                                                              {
					                                                              "hr", () => new HrRender()
				                                                              },
				                                                              {
					                                                              "br", () => new LineBreakRender()
				                                                              },
				                                                              {
					                                                              "ClearLine", () => new ClearLineRender()
				                                                              },
				                                                              {
					                                                              "c", () => new ColorRender()
				                                                              },
				                                                              {
					                                                              "s", () => new StyleRender()
				                                                              },
				                                                              {
					                                                              "if", () => new IfRender()
				                                                              },
				                                                              {
					                                                              "ifnot", () => new IfNotRender()
				                                                              },
				                                                              {
					                                                              "with", () => new WithRender()
				                                                              },
				                                                              {
					                                                              "foreach", () => new ForEachRender()
				                                                              },
			                                                              };

		public Renderer Create(Template template, Token token)
		{
			var strings = token.Substring.Split(new[]
				                                    {
					                                    ':'
				                                    }, 2);
			var command = strings.First();
			var isClosed = command.EndsWith("/");
			command = command.Trim(' ', '/');
			var config = strings.Length == 2 ? strings[1] : string.Empty;
			var renderer = this.Commands[command]();

			renderer.Config = config;

			// renderer.SubRenderes = subRenderes.ToArray();
			renderer.Template = template;
			renderer.IsClosed = isClosed;
			return renderer;
		}
	}
}