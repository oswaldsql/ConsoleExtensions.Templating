namespace ConsoleExtensions.Templating.Token
{
    internal class EndCommandToken : Token
    {
        public EndCommandToken(string substring)
            : base(substring)
        {
        }

        public override TokenType Type => TokenType.EndCommand;

        public override string ToString()
        {
            return "endCom:" + base.ToString();
        }
    }
}