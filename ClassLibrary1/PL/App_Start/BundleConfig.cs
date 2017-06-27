using System.Web;
using System.Web.Optimization;

namespace PL
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
            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                        "~/Scripts/DataTables/jquery.dataTables.min.js",
                        "~/Scripts/DataTables/jquery.dataTables.rowReordering.js"));

            bundles.Add(new ScriptBundle("~/bundles/jQueryUI").Include(
                        "~/Scripts/jquery-ui-1.12.1.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/highcharts").Include(
                        "~/Scripts/highstock.js",
                        "~/Scripts/highcharts.js"));


            bundles.Add(new ScriptBundle("~/bundles/myScriptLayot").Include(
                        "~/Scripts/myScript/layout.js"));

            bundles.Add(new ScriptBundle("~/bundles/myScriptViewSave").Include(
                        "~/Scripts/myScript/saveView.js"));

            bundles.Add(new ScriptBundle("~/bundles/myScriptPositionSave").Include(
                        "~/Scripts/myScript/savePosition.js"));

            bundles.Add(new ScriptBundle("~/bundles/myScriptProfileSave").Include(
                        "~/Scripts/myScript/saveProfile.js"));

            bundles.Add(new ScriptBundle("~/bundles/myScriptViewTampleteSave").Include(
                        "~/Scripts/myScript/saveViewTamplete.js"));

            bundles.Add(new ScriptBundle("~/bundles/myScriptViewTampleteColumnSave").Include(
                        "~/Scripts/myScript/saveViewColumnTamplete.js"));

            bundles.Add(new ScriptBundle("~/bundles/myScriptTampleteManagement").Include(
                        "~/Scripts/myScript/tamplete-management.js"));

            bundles.Add(new ScriptBundle("~/bundles/myScriptTampleteColumnManagement").Include(
                        "~/Scripts/myScript/tamplete-column-managemen.js"));

            bundles.Add(new ScriptBundle("~/bundles/myScriptViewManagement").Include(
                        "~/Scripts/myScript/view-management.js"));

            bundles.Add(new ScriptBundle("~/bundles/myScriptCustomerManagement").Include(
                        "~/Scripts/myScript/customer-management.js"));

            bundles.Add(new ScriptBundle("~/bundles/myScriptProfileManagement").Include(
                        "~/Scripts/myScript/profile-management.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-select").Include(
                        "~/Scripts/bootstrap-select.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/myStylelayout").Include(
                      "~/Content/myStyle/layout.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-select").Include(
                      "~/Content/bootstrap-select.css"));

            bundles.Add(new StyleBundle("~/Content/DataTables").Include(
                      "~/Content/DataTables/css/jquery.dataTables.css"));

            bundles.Add(new StyleBundle("~/Content/jQueryUI").Include(
                      "~/Content/themes/base/all.css"));
        }
    }
}
