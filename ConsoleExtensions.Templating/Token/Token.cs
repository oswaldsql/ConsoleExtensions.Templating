namespace ConsoleExtensions.Templating.Token
{
    internal abstract class Token
    {
        internal readonly string Substring;

        public Token(string substring)
        {
            this.Substring = substring;
        }

        public abstract TokenType Type { get; }

        public override string ToString()
        {
            return this.Substring;
        }
    }
}