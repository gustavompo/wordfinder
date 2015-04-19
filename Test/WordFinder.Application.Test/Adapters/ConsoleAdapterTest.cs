using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using WordFinder.Application.Adapters;

namespace WordFinder.Application.Test
{
    [TestClass]
    public class ConsoleAdapterTest
    {
        private string _aReallyFunnyMessage = "consoles are funny";
        [TestMethod]
        public void ShouldWriteLineToConsoleOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (StringReader sr = new StringReader(string.Empty))
                {
                    Console.SetIn(sr);
                    new ConsoleAdapter().WriteLine(_aReallyFunnyMessage);
                    var expected = string.Format("{0}{1}", _aReallyFunnyMessage, Environment.NewLine);
                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }
        }

        [TestMethod]
        public void ShouldReadLineFromConsoleInput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (StringReader sr = new StringReader(_aReallyFunnyMessage))
                {
                    Console.SetIn(sr);
                    var result = new ConsoleAdapter().ReadLine();
                    Assert.AreEqual<string>(_aReallyFunnyMessage, result);
                }
            }
        }
    }
}
