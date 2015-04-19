using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordFinder.WebserviceRepository.Adapters;
using WordFinder.WebserviceRepository;
using WordFinder.Entities;
using System.IO;

namespace WordFinder.Test
{
    [TestClass]
    public class FinderTest
    {
        private Finder _finder;

        [TestInitialize]
        public void Init()
        {
            _finder = new Finder(new WordRepository(new ConfigurationManagerAdapter()));
        }


        [TestMethod]
        public void ShouldFindOneOfTheFirstsWords()
        {
            var result = _finder.FindWord("ABA");
            AssertAcceptableResultForExistentWord(result);
        }

        [TestMethod]
        public void ShouldFindOneOfTheLastsWords()
        {
            var result = _finder.FindWord("ZURRO");
            AssertAcceptableResultForExistentWord(result);
        }

        [TestMethod]
        public void ShouldFindTheAWordStartingWithB()
        {
            var result = _finder.FindWord("BORRIFO");
            AssertAcceptableResultForExistentWord(result);
        }

        [TestMethod]
        public void ShouldFindTheAWordStartingWithX()
        {
            var result = _finder.FindWord("XAROPADA");
            AssertAcceptableResultForExistentWord(result);
        }

        [TestMethod]
        public void ShouldFindTheAWordStartingWithJ()
        {
            var result = _finder.FindWord("JUGO");
            AssertAcceptableResultForExistentWord(result);
        }

        [TestMethod]
        public void ShouldFindTheAWordStartingWithR()
        {
            var result = _finder.FindWord("RESTITUEM");
            AssertAcceptableResultForExistentWord(result);
        }

        [TestMethod]
        public void ShouldFindTheAWordWithDiacritics()
        {
            var result = _finder.FindWord("RESTRIÇÃO");
            AssertAcceptableResultForExistentWord(result);
        }

        [TestMethod]
        public void ShouldReturnErrorCodeForWordNotFound()
        {
            var result = _finder.FindWord("FUNNY_INEXISTENT_WORD");
            Assert.AreEqual(result.ErrorCode, WordFindingResult.ErrorCodes.NOT_FOUND);
        }

        private void AssertAcceptableResultForExistentWord(WordFindingResult result)
        {
            Assert.IsTrue(result.WordIndex > 0);
            #if DEBUG
            LogToAppFolderFile(result);
            #endif
        }
        private void LogToAppFolderFile(WordFindingResult result)
        {
            try
            {
                File.AppendAllLines(@"catsKilled.log", new[] { string.Format("At [{0}] you killed [{1}] cats to find the word [{2}], or simply to get the [{3}] error code.", DateTime.Now.ToShortTimeString(), result.WebservicesCallCount, result.WordFound, result.ErrorCode) });
            }
            catch (Exception) { }
        }
    }
}
