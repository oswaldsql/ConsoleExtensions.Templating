namespace ConsoleExtensions.Templating.Token
{
	internal class CommandToken : Token
	{
		public CommandToken(string substring)
			: base(substring)
		{
		}

		public override TokenType Type => TokenType.Command;

		public override string ToString()
		{
			return "com:" + base.ToString();
		}
	}
}