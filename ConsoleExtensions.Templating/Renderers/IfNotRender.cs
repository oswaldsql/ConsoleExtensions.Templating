namespace ConsoleExtensions.Templating.Renderers
{
	internal class IfNotRender : IfRender
	{
		internal override bool IsTruthy(object o)
		{
			return !base.IsTruthy(o);
		}
	}
}