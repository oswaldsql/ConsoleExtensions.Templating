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

			this.TypeTemplates = new Dictionary<Type, Template>();
			this.TypeConverters = new Dictionary<Type, Func<object, object>>();
		}

		internal Dictionary<Type, Template> TypeTemplates { get; }

		public void AddTypeTemplate<T>(string source, Func<T, object> typeConverter = null)
		{
			if (source != null || source == "{}" || source == "{.}")
			{
				var template = this.Parse(source);
				this.TypeTemplates[typeof(T)] = template;
			}
			else
			{
				if (this.TypeTemplates.ContainsKey(typeof(T)))
				{
					this.TypeTemplates.Remove(typeof(T));
				}
			}

			if (typeConverter != null)
			{
				this.TypeConverters[typeof(T)] = obj => typeConverter((T)obj);
			}
		}

		public Dictionary<Type,Func<object, object>> TypeConverters { get; }

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
			var result = new Template(this);

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