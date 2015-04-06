using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using React;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Bade.Admin.ReactConfig), "Configure")]

namespace Bade.Admin
{
    public static class ReactConfig
    {
        public static void Configure()
        {



            ReactSiteConfiguration.Configuration
                .AddScript("~/app/prod/serverBundle.js");








        }
    }
}