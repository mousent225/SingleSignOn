using System.Web;
using System.Web.Optimization;

namespace SingleSignOn
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         //"~/Scripts/jquery-{version}.js"
                         "~/Scripts/jquery-1.10.2.min.js"
                         ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Scripts/kendo/kendo.all.min.js",
                "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                "~/Content/kendo/kendo.common-bootstrap.min.css",
                "~/Content/kendo/kendo.bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/freeow").Include(
                        "~/Scripts/jquery.freeow.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqwidgets").Include(
                        "~/Content/jqwidgets/jqwidgets/jqxcore.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxcore.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxdata.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxbuttons.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxscrollbar.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxlistbox.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxdatatable.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxdropdownlist.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxpanel.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxradiobutton.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxinput.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxdatetimeinput.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxcalendar.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxcheckbox.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxdata.export.js"//jqxtextarea
                        , "~/Content/jqwidgets/jqwidgets/jqxtextarea.js"
                         , "~/Content/jqwidgets/jqwidgets/jqxnumberinput.js"
                        ));//implement grid
            bundles.Add(new ScriptBundle("~/bundles/jqwidgetsGrid").Include(
                        "~/Content/jqwidgets/jqwidgets/jqxgrid.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxmenu.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxgrid.sort.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxgrid.selection.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxgrid.pager.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxgrid.filter.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxgrid.edit.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxdata.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxdata.export.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxgrid.export.js"                        
                        , "~/Content/jqwidgets/jqwidgets/jqxgrid.columnsresize.js"
                       ));//combox
            bundles.Add(new ScriptBundle("~/bundles/jqwidgetsCombobox").Include(
                "~/Content/jqwidgets/jqwidgets/jqxcombobox.js"
                 , "~/Content/jqwidgets/jqwidgets/jqxdropdownbutton.js"
                ));

            //jqxTabs
            bundles.Add(new ScriptBundle("~/bundles/jqxtabs").Include(
                        "~/Content/jqwidgets/jqwidgets/jqxtabs.js"
                            ));

            //jqxPasswordInput
            bundles.Add(new ScriptBundle("~/bundles/jqxPasswordInput").Include(
                        "~/Content/jqwidgets/jqwidgets/jqxPasswordInput.js",
                        "~/Content/jqwidgets/jqwidgets/jqxtooltip.js"
                            ));
            //split panel
            bundles.Add(new ScriptBundle("~/bundles/jqxsplitter").Include(
                        "~/Content/jqwidgets/jqwidgets/jqxsplitter.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxexpander.js"
                            ));
            //file upload
            bundles.Add(new ScriptBundle("~/bundles/jqxfileupload").Include(
                        "~/Content/jqwidgets/jqwidgets/jqxfileupload.js"
                            ));
            //editor
            bundles.Add(new ScriptBundle("~/bundles/jqxeditor").Include(
                        "~/Content/jqwidgets/jqwidgets/jqxeditor.js"
                            ));
            //jqxdatetimeinput
            bundles.Add(new ScriptBundle("~/bundles/jqxdatetimeinput").Include(
                        "~/Content/jqwidgets/jqwidgets/jqxdatetimeinput.js"
                        , "~/Content/jqwidgets/jqwidgets/jqxcalendar.js"
                            ));
            //tree
            bundles.Add(new ScriptBundle("~/bundles/jqxtree").Include(
                        "~/Content/jqwidgets/jqwidgets/jqxtree.js"
                            ));
            //ckeditor
            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include("~/Content/ckeditor/ckeditor.js"
                            ));
            bundles.Add(new ScriptBundle("~/bundles/adapters/jquery").Include("~/Content/ckeditor/adapters/jquery.js"
                            ));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/common.js",
                "~/Scripts/dhtmlxcalendar.js",
                "~/Scripts/tab.js",
                "~/Scripts/bootstrap-modal.js",
                "~/Scripts/bootstrap-modalmanager.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/freeow/freeow.css",
                "~/Content/hsstyle/font-awesome.min.css", 
                "~/Content/hsstyle/bootstrap-modal.css",
                "~/Content/mainstyle/dhtmlxcalendar.css",
                "~/Content/custom-style.css"));

            bundles.Add(new ScriptBundle("~/bundles/common-js").Include(
                "~/Scripts/common-script.js"));

            bundles.Add(new StyleBundle("~/Content/login/hsstyle").Include(
                "~/Content/hsstyle/button.css",
                "~/Content/hsstyle/font-awesome.min.css"));
        }
    }
}
