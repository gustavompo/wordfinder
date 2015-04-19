using System;
using TechTalk.SpecFlow;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using WordFinder.Application;
using System.Configuration;
using WordFinder.Entities;

namespace WordFinder.ApplicationSpec.Steps
{
    [Binding]
    public class FindWordSteps
    {
        private string _wordToFind;
        private string _resultMessage;

        [Given(@"A valid existent word")]
        public void GivenAValidExistentWord()
        {
            _wordToFind = ConfigurationManager.AppSettings["existent_word"];
        }

        [When(@"I try to find the word")]
        public void WhenITryToFindTheWord()
        {
            _resultMessage = Infrasctucture.ApplicationConfig.ExecuteWithWord(_wordToFind);
        }

        [Then(@"the result message should be ok")]
        public void ThenTheResultMessageShouldBeOk()
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(_resultMessage));
        }

        [Then(@"the result must contain the index of the word")]
        public void ThenTheResultMustContainTheIndexOfTheWord()
        {
            var positionRegex = new Regex(@"\[(\d+)\] position");
            var position = int.Parse(positionRegex.Match(_resultMessage).Groups[1].Value);
            Assert.IsTrue(position >= 0 && position < int.MaxValue);
        }

        [Then(@"the result must contain the calls count")]
        public void ThenTheResultMustContainTheCallsCount()
        {
            var positionRegex = new Regex(@"killed \[(\d+)\] cats");
            var position = int.Parse(positionRegex.Match(_resultMessage).Groups[1].Value);
            Assert.IsTrue(position >= 0 && position < int.MaxValue);
        }

        [Given(@"An inexistent word")]
        public void GivenAnInexistentWord()
        {
            _wordToFind = ConfigurationManager.AppSettings["inexistent_word"];
        }

        [Then(@"the result must contain the error code of inexistent word")]
        public void ThenTheResultMustContainTheErrorCodeOfInexistentWord()
        {
            Assert.IsTrue(_resultMessage.Contains(WordFindingResult.ErrorCodes.NOT_FOUND));
        }

    }
}
