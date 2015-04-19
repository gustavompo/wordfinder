using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordFinder.Entities.Internal
{
    public class WordFindingLimit
    {
        public WordFindingLimit(int index, Word word)
        {
            this.Index = index;
            this.Word = word;
        }

        public Word Word { get; private set; }

        public int Index { get; private set; }
    }
}
