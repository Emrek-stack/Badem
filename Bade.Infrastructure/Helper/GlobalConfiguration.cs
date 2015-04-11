using System.Configuration;
using System.Web;

namespace Bade.Infrastructure.Helper
{
    public class GlobalConfiguration
    {
        private static AppSettingsSection _appSettings;

        public static string DefaultConnectionStringName
        {
            get { return _appSettings.Settings["DefaultConnectionString"].Value; }
        }


        public static void LoadConfiguration()
        {
            var aa = HttpRuntime.AppDomainAppPath;

            ExeConfigurationFileMap efm = new ExeConfigurationFileMap { ExeConfigFilename = string.Format(@"{0}\bin\Application.config", HttpRuntime.AppDomainAppPath) };
            System.Configuration.Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(efm, ConfigurationUserLevel.None);
            if (!configuration.HasFile) return;
            _appSettings = configuration.AppSettings;
            //KeyValueConfigurationElement element = appSettings.Settings["myconfig"];
            //Console.WriteLine(element.Value);
            //Console.ReadKey(true);
        }
    }

}