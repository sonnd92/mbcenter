using System.Web;
using System.Web.Optimization;

namespace Web365
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/swiper.css",
                      "~/Content/animate.css",
                      "~/Content/jquery.fancybox.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/treset.css",
                      "~/Content/tstyle.css",
                      "~/Content/jquery.toast.css"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/js/libs/jquery-1.11.2.js",
                        "~/Scripts/js/libs/bootstrap.js",
                        "~/Scripts/js/swiper.js",
                        "~/Scripts/js/wow.js",
                        "~/Scripts/js/jquery.toast.js",
                        "~/Scripts/js/jquery.fancybox.pack.js",
                        "~/Scripts/js/moment.js",
                        "~/Scripts/js/bootstrap-datetimepicker.js",
                        "~/Scripts/js/swiper.js",
                        "~/Scripts/js/functions.js",
                        "~/Scripts/js/libs/ie-emulation-modes-warning.js"));

        }
    }
}
