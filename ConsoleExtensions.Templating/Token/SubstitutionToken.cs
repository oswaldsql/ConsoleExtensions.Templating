namespace ConsoleExtensions.Templating.Token
{
	internal class SubstitutionToken : Token
	{
		public SubstitutionToken(string substring)
			: base(substring)
		{
		}

		public override TokenType Type => TokenType.Substitution;

		public override string ToString()
		{
			return "sub:" + base.ToString();
		}
	}
}