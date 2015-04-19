using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordFinder.Application.Adapters;
using WordFinder.Entities;
using WordFinder.Repository;
using WordFinder.WebserviceRepository;
using WordFinder.WebserviceRepository.Adapters;

namespace WordFinder.Application
{
    public class WordFinderConsoleApplication
    {
        /// <summary>
        /// For a more sophisticated environment, it would be better to use a Dependency Injection framework for initialization
        /// For this simple application, it's deffinelly overhead (and overengeneering)
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var wordToFind = (args.Length > 0) ? args[0] : string.Empty;

            if(string.IsNullOrWhiteSpace(wordToFind)){
                Console.WriteLine(Messages.ENTER_WORD_TO_FIND);
                wordToFind = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(wordToFind))
                    return;
            }

            var wordFindingResult = Finder.FindWord(wordToFind);
            Console.WriteLine(MessageFor(wordFindingResult, wordToFind));
            Console.ReadLine();
        }

        private static string MessageFor(WordFindingResult result, string wordToFind)
        {
            if (result.WordIndex > 0)
                return string.Format(Messages.WORD_FOUND, result.WebservicesCallCount, result.WordFound, result.WordIndex);
            return string.Format(Messages.WORD_NOT_FOUND, result.WebservicesCallCount, wordToFind, result.ErrorCode);
        }

        private static IFinder _wordFinder = new Finder(new WordRepository(new ConfigurationManagerAdapter()));
        public static IFinder Finder
        {
            get { return _wordFinder; }
            set { _wordFinder = value; }
        }

        private static IConsoleAdapter _consoleAdapter = new ConsoleAdapter();
        public static IConsoleAdapter Console
        {
            get { return _consoleAdapter; }
            set { _consoleAdapter = value; }
        }

    }
}
