using Bade.Admin;
using React;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(ReactConfig), "Configure")]

namespace Bade.Admin
{
    public static class ReactConfig
    {
        public static void Configure()
        {
            ReactSiteConfiguration.Configuration
                .SetUseHarmony(true)
                .SetReuseJavaScriptEngines(true)
                .SetStripTypes(true)
                .AddScript("~/app/components/MiniButton.js");
        }
    }
}