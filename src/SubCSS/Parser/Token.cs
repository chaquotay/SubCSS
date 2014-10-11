namespace SubCSS.Parser
{
    internal class Token
    {

        private readonly TokenType _tokenType;

        public int Position
        {
            get { return _position; }
        }

        public TokenType TokenType
        {
            get { return _tokenType; }
        }
        private readonly string _tokenText;
        private readonly int _position;

        public string TokenText
        {
            get { return _tokenText; }
        }

        public Token(TokenType tokenType, string tokenText, int position)
        {
            this._tokenText = tokenText;
            this._position = position;
            this._tokenType = tokenType;
        }

    }
}