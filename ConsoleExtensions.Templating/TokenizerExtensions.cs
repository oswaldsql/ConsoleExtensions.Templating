namespace ConsoleExtensions.Templating
{
    using System.Collections.Generic;

    using ConsoleExtensions.Templating.Token;

    internal static class TokenizerExtensions
    {
        public static IEnumerable<Token.Token> Optimize(this IEnumerable<Token.Token> source)
        {
            RawTextToken prevToken = null;
            foreach (var current in source)
            {
                if (current is RawTextToken)
                {
                    prevToken = new RawTextToken(prevToken, current);
                }
                else
                {
                    if (prevToken != null)
                    {
                        yield return prevToken;
                    }

                    prevToken = null;
                    yield return current;
                }
            }

            if (prevToken != null)
            {
                yield return prevToken;
            }
        }
    }
}