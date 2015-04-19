using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using WordFinder.Repository;
using WordFinder.Repository.Exceptions;
using WordFinder.WebserviceRepository.Adapters;

namespace WordFinder.WebserviceRepository
{
    public class WordRepository : IWordRespository
    {
        public static readonly string SERVICE_URL_CONFIG_KEY = "dictionary-webservice-url";

        private  IConfigurationAdapter _configurationAdapter;
        public WordRepository (IConfigurationAdapter configurationAdapter)
	    {
            _configurationAdapter = configurationAdapter;
	    }

        private string ServiceUrl
        {
            get
            {
                return _configurationAdapter.Config(SERVICE_URL_CONFIG_KEY) ?? "http://teste.way2.com.br/dic/api/words/";
            }
        }

        public string WordAt(int index)
        {

            var client = new RestClient(ServiceUrl);
            var request = new RestRequest("{index}", Method.GET);
            request.AddUrlSegment("index", index.ToString());
            var result = client.Execute(request);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return result.Content;
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new RepositoryArgumentException(string.Format("unable to retrieve word at index [{0}]: [{1}]", index, ServiceUrl));
            }
            else
            {
                throw new Exception(string.Format("unable to connect to the webservice. the returned status code is [{0}]", result.StatusCode));
            }

        }
    }
}
