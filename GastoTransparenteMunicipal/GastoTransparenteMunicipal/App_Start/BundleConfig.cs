using System.Web;
using System.Web.Optimization;

namespace GastoTransparenteMunicipal
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            
            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap-reboot.css",
                      "~/Content/css/bootstrap-grid.css",
                      "~/Content/css/styleBootrap.css",
                      "~/Content/css/font.css",
                      "~/Content/css/style.css",
                      "~/Content/css/tinyscrollbar.css",
                      "~/Content/site.css"));      

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            
        }
    }
}
