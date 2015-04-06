using System.Configuration;
using Bade.Lib.Configuration;

namespace Bade.Infrastructure.Configuration
{
    public class ConfigReader : IConfigReader
    {
        public int ApplicationId
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ApplicationId"]); }
        }

        public string DefaultConnectionString
        {
            get { return ConfigurationManager.AppSettings["DefaultConnectionString"]; }
        }

    }
}