using System.Configuration;

namespace Bade.Lib.Configuration
{
    public class GlobalConfiguration
    {
        public int ApplicationId
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ApplicationId"]); }
        }

    }
}