using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordFinder.Test
{
    [TestClass]
    public class WordTest
    {
        [TestMethod]
        public void ShouldRemoveDiacriticsInSearchableWord()
        {
            var result = new Word("ATENÇÃO");
            Assert.AreEqual("ATENCAO", result.SearchableWordString);
        }

        [TestMethod]
        public void ShouldRemoveInvalidCharsInSearchableWord()
        {
            var result = new Word("FIND-ME-**-IF-YOU&&CAN");
            Assert.AreEqual("FINDMEIFYOUCAN", result.SearchableWordString);
        }

        [TestMethod]
        public void ShouldCalculateValidScoreForWordsAandB()
        {
            var wordA = new Word("A");
            var wordB = new Word("B");
            Assert.IsTrue(wordA.Score < wordB.Score);
        }

        [TestMethod]
        public void ShouldCalculateValidScoreForWordsAAZAandAAXAandBAAAandFUNNYandNOTFUNNY()
        {
            var wordAAXA = new Word("AAXA");
            var wordAAZA = new Word("AAZA");
            var wordBAAA = new Word("BAAA");
            var wordFUNNY = new Word("FUNNY");
            var wordNOTFUNNY = new Word("NOTFUNNY");

            Assert.IsTrue(wordAAXA.Score < wordAAZA.Score);
            Assert.IsTrue(wordAAZA.Score < wordBAAA.Score);
            Assert.IsTrue(wordBAAA.Score < wordFUNNY.Score);
            Assert.IsTrue(wordFUNNY.Score < wordNOTFUNNY.Score);
        }

        [TestMethod]
        public void ShouldCalculateSameScoreForEquivalentWordsWithAndWithoutDiacriticsAndInvalidChars()
        {
            var withDiacritics = new Word("FIND-ME-**-IF-YOU&&CANÃÊÍ");
            var withoutDiacritics = new Word("FINDMEIFYOUCANAEI");
            Assert.AreEqual(withDiacritics.Score, withoutDiacritics.Score);
        }
    }
}
