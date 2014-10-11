using System;
using System.Text.RegularExpressions;

namespace SubCSS.Parser
{
    internal class MatchableCharacterSequence
    {
        private readonly string completeText;
        private string text;
        private int position = 0;

        public MatchableCharacterSequence(string text)
        {
            this.text = text;
            this.completeText = text;
        }

        public Match Match(Regex regex)
        {
            var match = regex.Match(text);
            if(match.Success)
            {
                var length = match.Value.Length;
                Move(length);
            }
            return match;
        }

        public void Move(int relative)
        {
            position = position + relative;
            UpdateText();
        }

        public void MoveTo(int absolute)
        {
            if(absolute<0 || absolute >= completeText.Length) throw new Exception("invalid position: " + absolute);
            position = absolute;
            UpdateText();
        }

        private void UpdateText()
        {
            text = completeText.Substring(Position); // suboptimal, aber regex.Match(completeText, position) matcht nicht @"\A" bzw. "^".
        }

        public string RemainingText
        {
            get
            {
                return text;
            }
        }

        public bool EndReached
        {
            get
            {
                return string.Empty.Equals(text);
            }
        }

        public int Position
        {
            get { return position; }
        }
    }
}