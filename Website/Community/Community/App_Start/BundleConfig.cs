using System.Web;
using System.Web.Optimization;

namespace Community
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*",
                        "~/ckeditor/ckeditor.js",
                        "~/Scripts/global.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Fonts/font-awesome/css/font-awesome.min.css",
                      "~/Content/site.css",
                      "~/Content/jquery-ui.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/Chart").Include(
                    "~/Scripts/Chart.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Swiper").Include(
                "~/Scripts/Swiper/swiper.min.js",
                "~/Scripts/Swiper/swiper.jquery.min.js"));

            bundles.Add(new StyleBundle("~/Content/Swiper").Include(
                "~/Content/Swiper/swiper.min.css"));
        }
    }
}
