using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WordFinder.Application.Adapters;
using WordFinder.Entities;

namespace WordFinder.Application.Test
{
    [TestClass]
    public class WordFinderConsoleApplicationTest
    {
        private Mock<IConsoleAdapter> _consoleAdapterMock;
        private Mock<IFinder> _wordFinderMock;
        private string _validWord = "FUNNYWORD";
        private int _callCount = 5;
        private string _errorCode = "FUNNYERROR";
        private int _wordIndexForValidResult = 44;
        
        [TestInitialize]
        public void Init()
        {
            _wordFinderMock = new Mock<IFinder>();
            _wordFinderMock
                .Setup(e => e.FindWord(It.IsAny<string>()))
                .Returns(GivenFoundWord());

            _consoleAdapterMock = new Mock<IConsoleAdapter>();
            _consoleAdapterMock
                .Setup(e => e.ReadLine())
                .Returns(_validWord);

            WordFinderConsoleApplication.Console = _consoleAdapterMock.Object;
            WordFinderConsoleApplication.Finder = _wordFinderMock.Object;
        }

        [TestMethod]
        public void ShouldExitApplicationGivenNoWordsToFind()
        {
            GivenNoWordsToFind();
            WordFinderConsoleApplication.Main(new string[] { });
            _consoleAdapterMock.Verify(e => e.ReadLine(), Times.Once);
            _consoleAdapterMock.Verify(e => e.WriteLine(Messages.ENTER_WORD_TO_FIND), Times.Once);
            _consoleAdapterMock.Verify(e => e.WriteLine(It.IsAny<string>()), Times.Once);
            _wordFinderMock.Verify(e => e.FindWord(It.IsAny<string>()), Times.Never);
        }


        [TestMethod]
        public void ShouldReturnCallsCountAndWordNameGivenFoundWord()
        {
            WordFinderConsoleApplication.Main(new string[] { _validWord });
            _consoleAdapterMock.Verify(e => e.ReadLine(), Times.Once);
            var expectedMessage = string.Format(Messages.WORD_FOUND, _callCount, _validWord, _wordIndexForValidResult);
            _consoleAdapterMock.Verify(e => e.WriteLine(expectedMessage), Times.Once);
            _consoleAdapterMock.Verify(e => e.WriteLine(It.IsAny<string>()), Times.Once);
            _wordFinderMock.Verify(e => e.FindWord(_validWord), Times.Once);
        }

        [TestMethod]
        public void ShouldReadConsoleIfNoWordInformedAsMainArgument()
        {
            WordFinderConsoleApplication.Main(new string[] { });
            _consoleAdapterMock.Verify(e => e.ReadLine(), Times.AtLeast(2));
        }


        [TestMethod]
        public void ShouldReturnWordNotFoundAndCallCoundAndErrorCodeGivenNotFoundWord()
        {
            GivenNotFoundWord();
            WordFinderConsoleApplication.Main(new string[] { _validWord });
            
            var expectedMessage = string.Format(Messages.WORD_NOT_FOUND, _callCount, _validWord, _errorCode);
            _consoleAdapterMock.Verify(e => e.WriteLine(expectedMessage), Times.Once);
            _consoleAdapterMock.Verify(e => e.WriteLine(It.IsAny<string>()), Times.Once);
            _consoleAdapterMock.Verify(e => e.ReadLine(), Times.Once);
            _wordFinderMock.Verify(e => e.FindWord(_validWord), Times.Once);
        }

        private void GivenNotFoundWord()
        {
            _wordFinderMock = new Mock<IFinder>();
            _wordFinderMock
                .Setup(e => e.FindWord(It.IsAny<string>()))
                .Returns(new WordFindingResult(_callCount, -1, string.Empty, _errorCode));

            WordFinderConsoleApplication.Finder = _wordFinderMock.Object;
        }

        private void GivenNoWordsToFind()
        {
            _consoleAdapterMock = new Mock<IConsoleAdapter>();
            _consoleAdapterMock
                .Setup(e => e.ReadLine())
                .Returns(string.Empty);

            WordFinderConsoleApplication.Console = _consoleAdapterMock.Object;
        }

        private WordFindingResult GivenFoundWord()
        {
            return new WordFindingResult(_callCount, _wordIndexForValidResult, _validWord);
        }

        
    }
}
