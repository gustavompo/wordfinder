using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordFinder.Repository.Exceptions
{
    public class RepositoryArgumentException : ArgumentException
    {
        public RepositoryArgumentException(string message) : base(message) { }
    }
}
