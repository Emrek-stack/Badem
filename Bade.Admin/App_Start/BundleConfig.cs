using System.Web;
using System.Web.Optimization;
using System.Web.Optimization.React;

namespace Bade.Admin
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {


            //bundles.Add(new Bundle("~/bundles/main", new IBundleTransform[]
            //{
            //    // This works the same as JsxBundle (transform then minify) but you could
            //    //add your own transforms as well.
            //    new JsxTransform()
            //})
            //    .Include(
            //        "~/app/components/ApplicationConfigPage.js",
            //        "~/app/components/LoadingIndicator.js"
            //    ));

            //bundles.Add(new Bundle("~/scripts/semantic.js", new IBundleTransform[]
            //{
            //    new JsMinify()
            //}).Include("~/assets/semantic-ui/"));


        }
    }
}