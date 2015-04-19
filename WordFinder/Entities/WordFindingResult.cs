using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordFinder.Entities
{
    public class WordFindingResult
    {
        public static class ErrorCodes
        {
            public static string NOT_FOUND = "WORD_NOT_FOUND";
            public static string CALLS_LIMIT_EXCEEDED = "CALLS_LIMIT_EXCEEDED";
        } 

        public WordFindingResult(int webservicesCallCount, int wordIndex, string wordFound)
        {
            WebservicesCallCount = webservicesCallCount;
            WordIndex = wordIndex;
            WordFound = wordFound;
        }

        public WordFindingResult(int webservicesCallCount, int wordIndex, string wordFound, string errorCode)
            : this(webservicesCallCount, wordIndex, wordFound)
        {
            ErrorCode = errorCode;
        }
        public int WebservicesCallCount { get; private set; }
        public int WordIndex { get; private set; }
        public string WordFound { get; private set; }
        public string ErrorCode { get; private set; }
    }

}
