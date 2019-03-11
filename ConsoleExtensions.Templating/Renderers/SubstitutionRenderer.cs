namespace ConsoleExtensions.Templating.Renderers
{
	using System;
	using System.Globalization;
	using System.Reflection;

	using ConsoleExtensions.Proxy;

	internal class SubstitutionRenderer : Renderer
	{
		private static readonly char[] FormatSeparator =
			{
				':'
			};

		private string format;

		public SubstitutionRenderer(string value, Template template)
		{
			this.Config = value;
			this.Template = template;
		}

		public string PropertyName { get; set; }

		internal override string Config
		{
			get => base.Config;
			set
			{
				base.Config = value;
				var strings = value.Split(FormatSeparator, 2);
				this.PropertyName = strings[0];
				if (strings.Length == 2)
				{
					this.format = strings[1];
				}
			}
		}

		public override void Render(IConsoleProxy proxy, object arg, CultureInfo culture)
		{
			var value = this.GetValueFromPropertyString(arg, this.PropertyName);
			if (value == null)
			{
				return;
			}

			if (this.format != null && value is IFormattable formatValue)
			{
				proxy.Write(formatValue.ToString(this.format, culture));
				return;
			}

			if (this.RenderUsingTypeTemplates(proxy, culture, value))
			{
				return;
			}

			proxy.Write(string.Format(culture, "{0}", value));
		}

		private bool RenderUsingTypeTemplates(IConsoleProxy proxy, CultureInfo culture, object value)
		{
			if (this.format != null) return false;

			var type = value.GetType();
			while (type != null)
			{
				if (this.Template.TypeTemplates.TryGetValue(type, out var template))
				{
					if (this.Template.TypeConverters.TryGetValue(type, out var converter))
					{
						value = converter(value);
					}

					proxy.WriteTemplate(template, value, culture);
					return true;
				}

				type = type.GetTypeInfo().BaseType;
			}

			return false;
		}
	}
}