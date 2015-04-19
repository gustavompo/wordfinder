
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordFinder.Entities;
using WordFinder.Entities.Internal;
using WordFinder.Repository;
using WordFinder.Repository.Exceptions;


namespace WordFinder
{
    public interface IFinder
    {
        WordFindingResult FindWord(string word);
    }
    public class Finder : IFinder
    {
        private IWordRespository _repository;
        public Finder(IWordRespository repository)
        {
            _repository = repository;
        }
        private readonly int _maximumCallsCount = 30;
        private readonly int _initialIndex = 10000;

        public WordFindingResult FindWord(string word)
        {
            var callsCount = 0;
            var lowerLimit = new WordFindingLimit(0, null);
            var upperLimit = new WordFindingLimit(int.MaxValue, null);
            var wordToFind = new Word(word);
            var wordIndex = _initialIndex;

            while (callsCount <= _maximumCallsCount)
            {
                if (CouldNotFindWord(lowerLimit, upperLimit))
                    return new WordFindingResult(callsCount, -1, string.Empty, WordFindingResult.ErrorCodes.NOT_FOUND);

                var wordFound = RetrieveWordAt(wordIndex);
                callsCount++;

                if (wordToFind.Equals(wordFound))
                    return new WordFindingResult(callsCount, wordIndex, wordFound.OriginalWordString);
                
                else if (IsIndexTooHigh(wordToFind, wordFound))
                {
                    upperLimit = new WordFindingLimit(wordIndex, wordFound);
                    wordIndex = IndexConsideringTooHighPreviousResult(lowerLimit, wordIndex);
                }
                else
                {
                    lowerLimit = new WordFindingLimit(wordIndex, wordFound);
                    wordIndex = IndexConsideringTooLowPreviousResult(lowerLimit, upperLimit, wordToFind);
                }
                
            }
            return new WordFindingResult(callsCount, -1, string.Empty, WordFindingResult.ErrorCodes.CALLS_LIMIT_EXCEEDED);
        }

        private Word RetrieveWordAt(int index)
        {
            var wordFound = new Word(null);
            try
            {
                wordFound = new Word(_repository.WordAt(index));
            }
            catch (RepositoryArgumentException) { }
            return wordFound;
        }

        private bool IsIndexTooHigh(Word wordToFind, Word wordReturned)
        {
            return wordReturned.Empty() || wordReturned.SearchableWordString.CompareTo(wordToFind.SearchableWordString) > 0;
        }
        private bool CouldNotFindWord(WordFindingLimit lowerLimit, WordFindingLimit upperLimit)
        {
            return lowerLimit.Index >= (upperLimit.Index - 1);
        }

        private int IndexConsideringTooHighPreviousResult(WordFindingLimit maxLowerLimit, int current)
        {
            return BinarySearch(maxLowerLimit.Index, current);
        }

        private int IndexConsideringTooLowPreviousResult(WordFindingLimit maxLowerLimit, WordFindingLimit minUpperLimit, Word target)
        {
            if (AreLowerAndUpperLimitsDefined(maxLowerLimit, minUpperLimit))
                return BinarySearch(maxLowerLimit.Index, minUpperLimit.Index);
            
            var scoreByIndexPosition = maxLowerLimit.Index / maxLowerLimit.Word.Score;
            var indexOfTargetBasedInScore = (int)(target.Score * scoreByIndexPosition);
            return indexOfTargetBasedInScore;
        }

        private bool AreLowerAndUpperLimitsDefined(WordFindingLimit lowerLimit, WordFindingLimit upperLimit)
        {
            return upperLimit.Index < int.MaxValue && lowerLimit.Index > 0;
        }
        private int BinarySearch(int lower, int upper)
        {
            return ((upper - lower) / 2) + lower;
        }
    }
}

