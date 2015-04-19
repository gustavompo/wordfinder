using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordFinder.WebserviceRepository.Adapters
{
    public interface IConfigurationAdapter
    {
        string Config(string key);
    }
}
