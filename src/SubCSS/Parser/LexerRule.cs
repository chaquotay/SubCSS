using System.Text.RegularExpressions;

namespace SubCSS.Parser
{
    internal class LexerRule
    {

        private readonly Regex _regex;

        public Regex Regex
        {
            get { return _regex; }
        }
        private readonly TokenType _tokenType;

        public TokenType TokenType
        {
            get { return _tokenType; }
        }

        public LexerRule(string pattern, TokenType tokenType)
        {
            this._regex = new Regex(pattern);
            this._tokenType = tokenType;
        }

    }
}