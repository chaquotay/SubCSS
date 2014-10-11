using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SubCSS.Ast;

namespace SubCSS.Parser
{
    internal class SubCssParser
    {
        private readonly SubCssLexer _lexer;

        private SubCssParser(SubCssLexer lexer)
        {
            _lexer = lexer;
        }

        public static StyleSheet Parse(string subCss)
        {
            return new SubCssParser(new SubCssLexer(new MatchableCharacterSequence(subCss))).Parse();
        }

        internal static ISelector ParseSelector(string sel)
        {
            return new SubCssParser(new SubCssLexer(new MatchableCharacterSequence(sel))).ParseSelector();
        }

        private StyleSheet Parse()
        {
            var ruleSets = RuleSets().ToList();
            ConsumeToken(TokenType.Eof);
            return new StyleSheet(ruleSets);
        }

        private ISelector ParseSelector()
        {
            var selector = Selectors();
            ConsumeToken(TokenType.Eof);
            return selector;
        }

        private IEnumerable<RuleSet> RuleSets()
        {
            while (LA(1) != TokenType.Eof)
            {
                var selector = Selectors();
                ConsumeToken(TokenType.LeftBrace);
                var properties = Properties().ToList();
                ConsumeToken(TokenType.RightBrace);
                yield return new RuleSet(selector, properties);
            }
        }

        private ISelector Selectors()
        {
            BaseSelector baseSelector = null;

            if (LA(1) == TokenType.Wildcard)
            {
                ConsumeToken(TokenType.Wildcard);
                baseSelector = new UniversalSelector();
            }
            else if (LA(1) == TokenType.Name)
            {
                var name = ConsumeToken(TokenType.Name);
                baseSelector = new TypeSelector(name.TokenText);
            }

            var subs = SubSelectors().ToList();

            return new SimpleSelector(baseSelector ?? new UniversalSelector(), subs);
        }

        private IEnumerable<RestrictingSelector> SubSelectors()
        {
            var next = LA(1);
            while (next == TokenType.Colon || next == TokenType.Dot)
            {
                if (next == TokenType.Colon)
                {
                    ConsumeToken(TokenType.Colon);
                    var pseudoClass = ConsumeToken(TokenType.Name).TokenText;
                    yield return new PseudoClassSelector(pseudoClass);
                }
                else if (next == TokenType.Dot)
                {
                    ConsumeToken(TokenType.Dot);
                    var clazz = ConsumeToken(TokenType.Name).TokenText;
                    yield return new ClassSelector(clazz);
                }
                next = LA(1);
            }
        }

        private IEnumerable<Property> Properties()
        {
            while (LA(1) != TokenType.RightBrace)
            {
                var property = Property();
                yield return property;

                if (LA(1) == TokenType.SemiColon)
                {
                    ConsumeToken(TokenType.SemiColon);
                }
            }
        }

        private Property Property()
        {
            var propName = ConsumeToken(TokenType.Name).TokenText;
            ConsumeToken(TokenType.Colon);
            var expr = Expression();
            var property = new Property(propName, expr, false);
            return property;
        }

        private object Expression()
        {
            
            var expr = OneExpression();
            if (LA(1) != TokenType.Comma)
                return expr;

            var expressions = new ArrayList();
            expressions.Add(expr);
            while (LA(1) == TokenType.Comma)
            {
                ConsumeToken(TokenType.Comma);
                expressions.Add(Expression());
            }
            return expressions;
        }

        private object OneExpression()
        {
            if (LA(1) == TokenType.DecimalNumber)
            {
                var num = ConsumeToken(TokenType.DecimalNumber).TokenText;
                var dec = Decimal.Parse(num, NumberStyles.Any, CultureInfo.InvariantCulture);
                var unit = Unit.None;
                var unitName = "";

                if (LA(1) == TokenType.Name)
                {
                    unitName = ConsumeToken(TokenType.Name).TokenText;
                    unit = ResolveUnit(unitName);
                }

                //return new Quantity(dec, unit);
                return dec.ToString(CultureInfo.InvariantCulture) + unitName;
            }

            if (LA(1) == TokenType.TextLiteral)
            {
                var t = ConsumeToken(TokenType.TextLiteral).TokenText;
                var text = t.Substring(1, t.Length - 2);
                return text;
            }

            if (LA(1) == TokenType.Color)
            {
                var t = ConsumeToken(TokenType.Color).TokenText;
                return t;
            }

            var tt = ConsumeToken(TokenType.Name).TokenText;
            return tt;
        }

        private Unit ResolveUnit(string unitName)
        {
            if(string.IsNullOrEmpty(unitName))
                return Unit.None;

            unitName = unitName.ToLowerInvariant();

            switch (unitName)
            {
                case "pt":
                    return Unit.Point;
                case "px":
                    return Unit.Pixel;
                default:
                    return Unit.Unknown;
            }
        }

        private readonly List<Token> _buffer = new List<Token>();

        private void ForceTokenIntoBuffer()
        {
            Token next = null;
            do
            {
                next = _lexer.NextToken();
            } while (next.TokenType == TokenType.Whitespace || next.TokenType == TokenType.LineComment || next.TokenType == TokenType.BlockComment);
            _buffer.Add(next);
        }

        private Token GetLookaheadToken(int k)
        {
            while (_buffer.Count < k)
            {
                ForceTokenIntoBuffer();
            }
            return _buffer[k - 1];
        }

        private Token ConsumeToken(TokenType tt)
        {
            if (_buffer.Count == 0)
            {
                ForceTokenIntoBuffer();
            }
            Token result = _buffer[0];
            if (result.TokenType != tt) throw new ParseException(result.TokenType, tt);
            _buffer.RemoveAt(0);
            return result;
        }
        
        private TokenType LA(int k)
        {
            return GetLookaheadToken(k).TokenType;
        }
    }
}