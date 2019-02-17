namespace ConsoleExtensions.Templating.Token
{
	internal class RawTextToken : Token
	{
		public RawTextToken(string substring)
			: base(substring)
		{
		}

		public RawTextToken(Token first, Token secound)
			: base(first?.Substring + secound.Substring)
		{
		}

		public override TokenType Type => TokenType.Text;
	}
}