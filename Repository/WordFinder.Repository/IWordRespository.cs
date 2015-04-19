using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordFinder.Repository
{
    public interface IWordRespository
    {
        string WordAt(int index);
    }
}
