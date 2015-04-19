using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordFinder.Application.Adapters
{

    public interface IConsoleAdapter
    {
        string ReadLine();
        void WriteLine(string line);
    }

    public class ConsoleAdapter : IConsoleAdapter
    {
        public string ReadLine() 
        {
            return Console.ReadLine();
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
