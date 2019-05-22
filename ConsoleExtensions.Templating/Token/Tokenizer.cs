namespace ConsoleExtensions.Templating.Token
{
    using System;
    using System.Collections.Generic;

    internal class Tokenizer
    {
        private static readonly Dictionary<char, Func<Iterator, Token>> Tokenizers;

        static Tokenizer()
        {
            Tokenizers = new Dictionary<char, Func<Iterator, Token>>
                             {
                                 { '[', TokenizeCommand }, { '{', TokenizeSubstitution }
                             };
        }

        public IEnumerable<Token> Tokenize(string source)
        {
            var iterator = new Iterator(source);

            while (!iterator.EOL)
            {
                var previousStart = iterator.Index;
                iterator.ResetStart();

                if (Tokenizers.TryGetValue(iterator.Current, out var parser))
                {
                    yield return parser(iterator);
                }
                else
                {
                    yield return RawToken(iterator);
                }

                if (previousStart == iterator.Index)
                {
                    throw new InvalidTemplateException();
                }
            }
        }

        private static Token RawToken(Iterator iterator)
        {
            iterator.IterateUntil('[', '{');

            return new RawTextToken(iterator.GetExternal());
        }

        private static Token TokenizeCommand(Iterator iterator)
        {
            if (iterator.Next == '[')
            {
                iterator.Iterate(2);
                return new RawTextToken("[");
            }

            iterator.IterateUntil(']');

            var c = iterator.GetInternal();
            return c.StartsWith("/") ? (Token)new EndCommandToken(c.Substring(1)) : new CommandToken(c);
        }

        private static Token TokenizeSubstitution(Iterator iterator)
        {
            if (iterator.Next == '{')
            {
                iterator.Iterate(2);
                return new RawTextToken("{");
            }

            iterator.IterateUntil('}');

            return new SubstitutionToken(iterator.GetInternal());
        }
    }
}