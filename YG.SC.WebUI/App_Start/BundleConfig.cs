using System.Web.Optimization;

namespace YG.SC.WebUI
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyleBundles(bundles);
            RegisterJavascriptBundles(bundles);
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css")
            .Include("~/Content/mycss/*.css")
            .Include("~/Content/*.css"));

            bundles.Add(new StyleBundle("~/fileuploadcss").Include("~/Content/jQuery-File-Upload/css/*.css"));
        }

        private static void RegisterJavascriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js")
                            .Include("~/Scripts/bootbox.js")
                            .Include("~/Scripts/bootstrap-timepicker.js")
                            .Include("~/Scripts/jquery.validate.js")
                            .Include("~/Scripts/Extension/language_CN.js")
                            .Include("~/Scripts/Extension/jQueryExtension.js")
                            .Include("~/Scripts/jquery.loadmask.js")
                            .Include("~/Scripts/Kendo/kendo.core.js")
                            .Include("~/Scripts/Kendo/kendo.data.js")
                            .Include("~/Scripts/Kendo/kendo.binder.js")
                            .Include("~/Scripts/webExpress/webExpress.js")
                            .Include("~/Scripts/webExpress/webExpress.utility.js")
                            .Include("~/Scripts/webExpress/webExpress.utility.string.js")
                            .Include("~/Scripts/webExpress/webExpress.utility.date.js")
                            .Include("~/Scripts/webExpress/webExpress.utility.accounting.js")
                            .Include("~/Scripts/webExpress/webExpress.utility.array.js")
                            .Include("~/Scripts/webExpress/webExpress.utility.ajax.js")
                            .Include("~/Scripts/webExpress/webExpress.ui.control.binders.js")
                            .Include("~/Scripts/Extension/kendo.Extension.js")
                            .Include("~/Scripts/common/se.js")
                            .Include("~/Scripts/chinaArea.js")
                            .Include("~/Scripts/chinaAreaRetriever.js")
                            .Include("~/Scripts/dynamicScript.js")
                            .Include("~/Scripts/views/base/se.ui.control.Eventable.js")
                            .Include("~/Scripts/views/base/se.ui.control.Pager.js")
                            .Include("~/Scripts/views/base/se.ui.control.ChinaArea.js")
                            .Include("~/Scripts/views/base/se.ui.view.Module.js")
                            .Include("~/Scripts/views/base/se.ui.view.EditModule.js")
                            .Include("~/Scripts/views/base/se.ui.view.ShopModal.js")
                            );

            bundles.Add(new ScriptBundle("~/fileuploadjs").Include("~/Content/jQuery-File-Upload/js/jquery.ui.widget.js")
                .Include("~/Content/jQuery-File-Upload/js/jquery.iframe-transport.js")
                .Include("~/Content/jQuery-File-Upload/js/jquery.fileupload.js"));
        }
    }
}