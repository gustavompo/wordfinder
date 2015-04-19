using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WordFinder.WebserviceRepository.Adapters;
using WordFinder.Repository.Exceptions;

namespace WordFinder.WebserviceRepository.Test
{
    /// <summary>
    /// Test class for WordRepository
    /// 
    /// For simplicity both integration and unit tests will be made here
    /// In a more complex environment/project, where integration test completion time impact in development process,
    /// separating unit from integration and even creating playlists of tests are a better solution.
    /// </summary>
    [TestClass]

    public class WordRepositoryTest
    {
        private Mock<IConfigurationAdapter> _configurationAdapterMock;
        private WordRepository _repository;

        [TestInitialize]
        public void Init()
        {
            _configurationAdapterMock = new Mock<IConfigurationAdapter>();
            _configurationAdapterMock
                .Setup(e => e.Config(WordRepository.SERVICE_URL_CONFIG_KEY))
                .Returns("http://teste.way2.com.br/dic/api/words/");

            _repository = new WordRepository(_configurationAdapterMock.Object);
        }

        [TestMethod]
        public void ShouldGetUrlFromConfigurationAdapter()
        {
            _repository.WordAt(0);
            _configurationAdapterMock
                .Verify(e=>e.Config(WordRepository.SERVICE_URL_CONFIG_KEY), Times.Once);
        }

        [TestMethod]
        public void ShouldRetrieveWordSuccessfully()
        {
            var result =_repository.WordAt(0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryArgumentException))]
        public void ShouldThrowRepositoryExceptionGivenTooHighIndex()
        {
            _repository.WordAt(int.MaxValue);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldThrowRepositoryExceptionGivenInvalidServiceUrl()
        {
            GivenInvalidServiceUrl();
            _repository.WordAt(0);
        }

        private void GivenInvalidServiceUrl()
        {
            _configurationAdapterMock
                .Setup(e => e.Config(WordRepository.SERVICE_URL_CONFIG_KEY))
                .Returns("http://funny-invalid-url.none/");

            _repository = new WordRepository(_configurationAdapterMock.Object);
        }

    }
}
