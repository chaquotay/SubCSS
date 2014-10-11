using System;
using System.Collections.Generic;

namespace SubCSS.Parser
{
    internal class SubCssLexer
    {
        private MatchableCharacterSequence characterSequence = null;

        // static, weil Regex Thread-Safe, siehe
        // - http://stackoverflow.com/questions/1664271/is-it-valid-to-create-a-static-regex-object-to-be-used-by-all-threads-in-the-asp
        // - http://msdn.microsoft.com/en-us/library/system.text.regularexpressions.regex.aspx
        private static List<LexerRule> lexerRules = new List<LexerRule> {
            new LexerRule("^//.*", TokenType.LineComment), 
            new LexerRule(@"^/\*([^\*]*\*)*?/", TokenType.BlockComment), 
            new LexerRule(@"^[\p{L}_][\p{L}0-9_@\-]*", TokenType.Name),
            new LexerRule(@"^\*", TokenType.Wildcard),
            //new LexerRule(@"^\'((\\.|[^\\])*?)\'", TokenType.TextLiteral), // Escape-Sequenz: '\x' oder 'y' für y!='\'
            new LexerRule("^\\\"[^\\\"]*\\\"", TokenType.TextLiteral), // Escape-Sequenz: '\x' oder 'y' für y!='\'
            new LexerRule(@"^([\s])+", TokenType.Whitespace),
            new LexerRule("^\\(", TokenType.LeftParenthesis),
            new LexerRule("^\\)", TokenType.RightParenthesis),
            new LexerRule("^\\{", TokenType.LeftBrace),
            new LexerRule("^\\}", TokenType.RightBrace),
            new LexerRule("^\\[", TokenType.LeftBracket),
            new LexerRule("^\\]", TokenType.RightBracket),
            new LexerRule("^\\.", TokenType.Dot),
            new LexerRule("^,", TokenType.Comma), 
            new LexerRule("^::", TokenType.ColonColon), 
            new LexerRule("^:", TokenType.Colon),
            new LexerRule("^\\+", TokenType.Plus),
            new LexerRule("^-", TokenType.Minus),
            new LexerRule("^;", TokenType.SemiColon),
            new LexerRule("^%", TokenType.Percent),
            new LexerRule("^!", TokenType.ExclMark),
            new LexerRule("^(0|([1-9][0-9]*))(\\.[0-9]+)?", TokenType.DecimalNumber),
            new LexerRule("^#([0-9a-fA-F])+", TokenType.Color),
            //new LexerRule("^", TokenType.Unknown)
        };

        public SubCssLexer(MatchableCharacterSequence seq)
        {
            this.characterSequence = seq;
        }

        private bool isEof = false;

        public Token NextToken()
        {
            if (isEof) throw new InvalidOperationException("EOF reached!");
            var position = characterSequence.Position;
            isEof = characterSequence.EndReached;
            if (isEof) return new Token(TokenType.Eof, string.Empty, position);

            foreach (var rule in lexerRules)
            {
                var m = characterSequence.Match(rule.Regex);
                if (m.Success)
                {
                    return new Token(rule.TokenType, m.Value, position);
                }
            }
            throw new Exception("no regex matched at: " + characterSequence.RemainingText);
        }
    }
}