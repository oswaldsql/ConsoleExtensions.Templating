namespace ConsoleExtensions.Templating
{
	using System;
	using System.Collections.Generic;

	using ConsoleExtensions.Proxy;
	using ConsoleExtensions.Templating.Renderers;
	using ConsoleExtensions.Templating.Token;

	public class TemplateParser
	{
		private CommandFactory factory;

		public TemplateParser()
		{
			this.factory = new CommandFactory();
			this.Style = new Dictionary<string, ConsoleStyle>(StringComparer.OrdinalIgnoreCase)
				             {
					             {
						             "Default", ConsoleStyle.Default
					             },
					             {
						             "Error", ConsoleStyle.Error
					             },
					             {
						             "Info", ConsoleStyle.Info
					             },
					             {
						             "Ok", ConsoleStyle.Ok
					             },
					             {
						             "Warning", ConsoleStyle.Warning
					             }
				             };

			this.SubTemplates = new Dictionary<Type, Template>();
		}

		internal Dictionary<Type, Template> SubTemplates { get; }

		public void AddSubTemplate<T>(string source)
		{
			if (source != null)
			{
				var template = this.Parse(source);
				this.SubTemplates[typeof(T)] = template;
			}
			else
			{
				if (this.SubTemplates.ContainsKey(typeof(T)))
				{
					this.SubTemplates.Remove(typeof(T));
				}
			}
		}

		public static TemplateParser Default { get; } = new TemplateParser();

		public Dictionary<string, ConsoleStyle> Style { get; }

		public Template Parse(string source)
		{
			var tokens = new Tokenizer().Tokenize(source).Optimize();

			var template = this.BuildTemplate(tokens);

			return template;
		}

		internal Template BuildTemplate(IEnumerable<Token.Token> tokens)
		{
			var result = new Template()
				             {
					             Styles = this.Style,
								 SubTemplates = this.SubTemplates
				             };

			var renderers = this.BuildRenderTree(result, tokens.GetEnumerator());

			result.RenderTree = new RootRenderer(renderers.ToArray());

			return result;
		}

		private Renderer BuildRenderer(Template template, IEnumerator<Token.Token> tokens, Token.Token token)
		{
			var renderer = this.factory.Create(template, token);
			if (!renderer.IsClosed)
			{
				var subRen = this.BuildRenderTree(template, tokens).ToArray();
				renderer.SubRenderes = subRen;
			}

			return renderer;
		}

		private List<Renderer> BuildRenderTree(Template template, IEnumerator<Token.Token> tokens)
		{
			var renderers = new List<Renderer>();

			while (tokens.MoveNext())
			{
				var token = tokens.Current;
				if (token.Type == TokenType.Text)
				{
					renderers.Add(new TextRenderer(token.Substring));
				}

				if (token.Type == TokenType.Substitution)
				{
					var renderer = new SubstitutionRenderer(token.Substring, template);
					renderers.Add(renderer);
				}

				if (token.Type == TokenType.Command)
				{
					var renderer = this.BuildRenderer(template, tokens, token);

					renderers.Add(renderer);
				}

				if (token.Type == TokenType.EndCommand)
				{
					renderers.Add(new EndRenderer(token.Substring));
					break;
				}
			}

			return renderers;
		}
	}
}