using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WordFinder.WebserviceRepository.Adapters
{
    public class ConfigurationManagerAdapter : IConfigurationAdapter
    {
        public string Config(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
