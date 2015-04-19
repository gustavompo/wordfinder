using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMLib.Extensions;
using System.Text.RegularExpressions;

namespace WordFinder
{
    public class Word
    {
        private string _originalWord;
        private string _searchableWord;
        private double? _score;
        
        public Word(string wordString)
        {
            
            _originalWord = wordString;
            _searchableWord = RemoveUnsearchableCharsAndMakeUpperCase(_originalWord);
        }

        public bool Empty()
        {
            return string.IsNullOrWhiteSpace(_originalWord);
        }

        public string SearchableWordString
        {
            get
            {
                return _searchableWord;
            }
        }
        public string OriginalWordString
        {
            get
            {
                return _originalWord;
            }
        }
        public double Score
        {
            get
            {
                if (_score == null)
                {
                    _score = GetScore();
                }
                return _score.Value;
            }
        }

        private static Regex _validCharsRegex;
        /// <summary>
        /// Regex instantiation is expensive, this singleton avoids instantiation overhead for each word
        /// </summary>
        private Regex ValidCharsRegex
        {
            get
            {
                if (_validCharsRegex == null)
                {
                    _validCharsRegex = new Regex("[a-zA-Z]");
                }
                return _validCharsRegex;
            }
        }

        /// <summary>
        /// Removes diacritics, all characters different from leters from A to Z and make it upperCase
        /// </summary>
        /// <param name="wordString"></param>
        /// <returns></returns>
        private string RemoveUnsearchableCharsAndMakeUpperCase(string wordString)
        {
            var searchableWord = _originalWord.RemoveDiacritics();
            if (string.IsNullOrWhiteSpace(searchableWord))
                return searchableWord;

            var upperWithoutDiacriticsWord = searchableWord
                                    .Trim(new char[] { '\uFEFF', '\u200B' })
                                    .ToUpperInvariant();
            var withoutInvalidChars = string.Empty;
            var matches = ValidCharsRegex.Matches(upperWithoutDiacriticsWord);
            foreach (var match in matches)
            {
                withoutInvalidChars += match.ToString();
            }
            return withoutInvalidChars;
        }

        /// <summary>
        /// Returns word's score based on searchable letters
        /// </summary>
        /// <returns></returns>
        private double GetScore()
        {
            if (string.IsNullOrWhiteSpace(_searchableWord))
                return 0;

            var aCharCode = ((int)'A');
            var zCharCode = ((int)'Z');
            var deltaFromAToZCharCode = zCharCode - aCharCode;
            var score = 0.0f;
            var multiplier = 1.0f;
            for (var i = 0; i < _searchableWord.Length; i++)
            {
                var currentChar = _searchableWord[i];
                score += ((((int)currentChar) - aCharCode) / multiplier);
                multiplier += deltaFromAToZCharCode;
            }
            return score;
        }

        public bool Equals(Word another)
        {
            return another != null &&
                string.Equals(this._searchableWord, another._searchableWord, StringComparison.InvariantCultureIgnoreCase);
        }

    }
}
