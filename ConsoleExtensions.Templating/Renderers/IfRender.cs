namespace ConsoleExtensions.Templating.Renderers
{
	using System;
	using System.Collections;
	using System.Globalization;

	using ConsoleExtensions.Proxy;

	internal class IfRender : Renderer
	{
		public override void Render(IConsoleProxy proxy, object arg, CultureInfo culture)
		{
			var o = this.GetValueFromPropertyString(arg, this.Config);

			var isTruthy = this.IsTruthy(o);
			if (isTruthy)
			{
				foreach (var subRenderer in this.SubRenderes)
				{
					subRenderer.Render(proxy, arg, culture);
				}
			}
		}

		internal virtual bool IsTruthy(object o)
		{
			switch (o)
			{
				case null:
					return false;
				case bool _:
					return (bool)o;
				case string _:
					return !string.IsNullOrEmpty(o as string);
				case IEnumerable _:
					return ((IEnumerable)o).GetEnumerator().MoveNext();
			}

			if (o is IConvertible convertible)
			{
				var d = convertible.ToDouble(CultureInfo.InvariantCulture);
				return Math.Abs(d) > 0;
			}

			return true;
		}
	}
}