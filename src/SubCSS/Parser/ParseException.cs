using System;
using System.Collections.Generic;

namespace SubCSS.Parser
{
    public class ParseException : Exception
    {

        public ParseException(TokenType found, params TokenType[] expected)
            :
                base(string.Format("expected: {0}, found: {1}", Join(expected), found))
        { }

        private static string Join(IEnumerable<TokenType> types)
        {
            return string.Join(", ",
                new List<TokenType>(types).ConvertAll<string>(delegate(TokenType tt) { return tt.ToString(); }).ToArray()
                );
        }

    }
}