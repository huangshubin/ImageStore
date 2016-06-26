using System.Web;
using System.Web.Optimization;

namespace ImageStoreWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Assets/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Assets/scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                   "~/Assets/scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Assets/scripts/bootstrap.js",
                      "~/Assets/scripts/respond.js"));

            bundles.Add(new StyleBundle("~/content/css").Include(
                      "~/Assets/css/bootstrap.css",
                      "~/Assets/css/app.css"));
        }
    }
}
