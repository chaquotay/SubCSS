namespace SubCSS.Parser
{
    public enum TokenType
    {
        Eof,
        Whitespace,
        Name,
        Wildcard,
        TextLiteral,
        ExtendedTextLiteral,
        ExtendedTextLiteralStart,
        ExtendedTextLiteralMiddle,
        ExtendedTextLiteralEnd,
        LeftParenthesis,
        RightParenthesis,
        LeftBrace,
        RightBrace,
        Dot,
        QmQm,
        ExclMark,
        Plus,
        PlusPlus,
        Number,
        DecimalNumber,
        Comparison,
        Equality,
        Colon,
        SemiColon,
        Qm,
        ColonColon,
        Comma,
        And,
        Or,
        Xor,
        Not,
        LeftBracket,
        RightBracket,
        Minus,
        Div,
        Mul,
        True,
        False,
        Null,
        Lambda,
        ShortLambda,
        Arrow,
        LineComment,
        BlockComment,
        Percent,
        Unknown,
        Color
    }
}