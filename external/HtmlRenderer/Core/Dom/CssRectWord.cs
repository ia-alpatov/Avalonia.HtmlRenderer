// "Therefore those skilled at the unorthodox
// are infinite as heaven and earth,
// inexhaustible as the great rivers.
// When they come to an end,
// they begin again,
// like the days and months;
// they die and are reborn,
// like the four seasons."
// 
// - Sun Tsu,
// "The Art of War"

using System;
using TheArtOfDev.HtmlRenderer.Adapters;
using TheArtOfDev.HtmlRenderer.Core.Utils;

namespace TheArtOfDev.HtmlRenderer.Core.Dom
{
    /// <summary>
    /// Represents a word inside an inline box
    /// </summary>
    internal sealed class CssRectWord : CssRect
    {
        #region Fields and Consts

        /// <summary>
        /// The word text
        /// </summary>
        private readonly ReadOnlyMemory<char> _text;

        /// <summary>
        /// was there a whitespace before the word chars (before trim)
        /// </summary>
        private readonly bool _hasSpaceBefore;

        /// <summary>
        /// was there a whitespace after the word chars (before trim)
        /// </summary>
        private readonly bool _hasSpaceAfter;

        #endregion


        /// <summary>
        /// Init.
        /// </summary>
        /// <param name="owner">the CSS box owner of the word</param>
        /// <param name="text">the word chars </param>
        /// <param name="hasSpaceBefore">was there a whitespace before the word chars (before trim)</param>
        /// <param name="hasSpaceAfter">was there a whitespace after the word chars (before trim)</param>
        public CssRectWord(CssBox owner, ReadOnlyMemory<char> text, bool hasSpaceBefore, bool hasSpaceAfter)
            : base(owner)
        {
            _text = text;
            _hasSpaceBefore = hasSpaceBefore;
            _hasSpaceAfter = hasSpaceAfter;
        }

        /// <summary>
        /// was there a whitespace before the word chars (before trim)
        /// </summary>
        public override bool HasSpaceBefore
        {
            get { return _hasSpaceBefore; }
        }

        /// <summary>
        /// was there a whitespace after the word chars (before trim)
        /// </summary>
        public override bool HasSpaceAfter
        {
            get { return _hasSpaceAfter; }
        }

        /// <summary>
        /// Gets a bool indicating if this word is composed only by spaces.
        /// Spaces include tabs and line breaks
        /// </summary>
        public override bool IsSpaces
        {
            get
            {
                foreach (var c in Text.Span)
                {
                    if (!char.IsWhiteSpace(c))
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Gets if the word is composed by only a line break
        /// </summary>
        public override bool IsLineBreak
        {
            get { return Text.Length == 1 && Text.Span[0] == '\n'; }
        }

        /// <summary>
        /// Gets the text of the word
        /// </summary>
        public override ReadOnlyMemory<char> Text
        {
            get { return _text; }
        }

           
        public bool IsEmoji { get; set; }

        public string FontFamily
        {
            get { return IsEmoji ? CssConstants.DefaultEmojiFont : this.OwnerBox.FontFamily; }
        }

        
        /// <summary>
        /// Represents this word for debugging purposes
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} ({1} char{2})", AsString.Replace(' ', '-').Replace("\n", "\\n"), Text.Length, Text.Length != 1 ? "s" : string.Empty);
        }
    }
}