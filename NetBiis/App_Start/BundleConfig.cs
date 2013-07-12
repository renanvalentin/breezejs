using System.Web;
using System.Web.Optimization;
using BundleTransformer.Core.Transformers;
using BundleTransformer.Core.Builders;

namespace NetBiis.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery",
                  "//ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js")
                  .Include("~/Scripts/jquery-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            var lessBundle = new Bundle("~/bundle/less").Include(
                "~/Content/less/core.less"
            );
            lessBundle.Builder = new NullBuilder();
            lessBundle.Transforms.Add(new CssTransformer());
            bundles.Add(lessBundle);
        }
    }
}