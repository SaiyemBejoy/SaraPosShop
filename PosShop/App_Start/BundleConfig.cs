using System.Web;
using System.Web.Optimization;

namespace PosShop
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js" ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"
                      ));

            //bundles.Add(new ScriptBundle("~/Admin/vendors/js").Include(
            //    "~/Content/vendors/popper.js/dist/umd/popper.js",
            //    "~/Content/vendors/bootstrap/dist/js/bootstrap.min.js",
            //    "~/Content/vendors/js-cookie/src/js.cookie.js",
            //    "~/Content/vendors/moment/min/moment.min.js",
            //    "~/Content/vendors/tooltip.js/dist/umd/tooltip.min.js",
            //    "~/Content/vendors/perfect-scrollbar/dist/perfect-scrollbar.js",
            //    "~/Content/vendors/wnumb/wNumb.js",
            //    "~/Content/vendors/jquery.repeater/src/lib.js",
            //    "~/Content/vendors/jquery.repeater/src/jquery.input.js",
            //    "~/Content/vendors/jquery.repeater/src/repeater.js",
            //    "~/Content/vendors/jquery-form/dist/jquery.form.min.js",
            //    "~/Content/vendors/block-ui/jquery.blockUI.js",
            //    "~/Content/vendors/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
            //    "~/Content/vendors/js/framework/components/plugins/forms/bootstrap-datepicker.init.js",
            //    "~/Content/vendors/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.js",
            //    "~/Content/vendors/bootstrap-maxlength/src/bootstrap-maxlength.js",
            //    "~/Content/vendors/bootstrap-switch/dist/js/bootstrap-switch.js",
            //    "~/Content/vendors/js/framework/components/plugins/forms/bootstrap-switch.init.js",
            //    "~/Content/vendors/vendors/bootstrap-multiselectsplitter/bootstrap-multiselectsplitter.min.js",
            //    "~/Content/vendors/bootstrap-select/dist/js/bootstrap-select.js",
            //    "~/Content/vendors/typeahead.js/dist/typeahead.bundle.js",
            //    "~/Content/vendors/handlebars/dist/handlebars.js",
            //    "~/Content/vendors/owl.carousel/dist/owl.carousel.js",
            //    "~/Content/vendors/autosize/dist/autosize.js",
            //    "~/Content/vendors/clipboard/dist/clipboard.min.js",
            //    "~/Content/vendors/jquery-validation/dist/jquery.validate.js",
            //    "~/Content/vendors/bootstrap-notify/bootstrap-notify.min.js",
            //    "~/Content/vendors/js/framework/components/plugins/base/bootstrap-notify.init.js",
            //    "~/Content/vendors/toastr/build/toastr.min.js",
            //    "~/Content/vendors/raphael/raphael.js",
            //    "~/Content/vendors/morris.js/morris.js",
            //    "~/Content/vendors/chartist/dist/chartist.js",
            //    "~/Content/vendors/chart.js/dist/Chart.bundle.js",
            //    "~/Content/vendors/js/framework/components/plugins/charts/chart.init.js",
            //    "~/Content/assets/vendors/base/vendors.bundle.js",
            //    "~/Content/assets/demo/base/scripts.bundle.js",
            //    "~/Content/assets/vendors/custom/fullcalendar/fullcalendar.bundle.js",
            //    "~/Content/assets/app/js/dashboard.js",
            //    "~/Content/assets/snippets/custom/pages/user/login.js"

            //  ));

            bundles.Add(new ScriptBundle("~/Admin/vendors/js")
                .Include("~/Content/vendors/popper.js/dist/umd/popper.js")
                .Include("~/Content/vendors/bootstrap/dist/js/bootstrap.min.js")
                .Include("~/Content/vendors/js-cookie/src/js.cookie.js")
                .Include("~/Content/vendors/moment/min/moment.min.js")
                .Include("~/Content/vendors/tooltip.js/dist/umd/tooltip.min.js")
                .Include("~/Content/vendors/perfect-scrollbar/dist/perfect-scrollbar.js")
                .Include("~/Content/vendors/wnumb/wNumb.js")
                .Include("~/Content/vendors/jquery.repeater/src/lib.js")
                .Include("~/Content/vendors/jquery.repeater/src/jquery.input.js")
                .Include("~/Content/vendors/jquery.repeater/src/repeater.js")
                .Include("~/Content/vendors/jquery-form/dist/jquery.form.min.js")
                .Include("~/Content/vendors/block-ui/jquery.blockUI.js")
                .Include("~/Content/vendors/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js")
                .Include("~/Content/vendors/js/framework/components/plugins/forms/bootstrap-datepicker.init.js")
                .Include("~/Content/vendors/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.js")
                .Include("~/Content/vendors/bootstrap-maxlength/src/bootstrap-maxlength.js")
                .Include("~/Content/vendors/bootstrap-switch/dist/js/bootstrap-switch.js")
                .Include("~/Content/vendors/js/framework/components/plugins/forms/bootstrap-switch.init.js")
                .Include("~/Content/vendors/vendors/bootstrap-multiselectsplitter/bootstrap-multiselectsplitter.min.js")
                .Include("~/Content/vendors/bootstrap-select/dist/js/bootstrap-select.js")
                .Include("~/Content/vendors/typeahead.js/dist/typeahead.bundle.js")
                .Include("~/Content/vendors/handlebars/dist/handlebars.js")
                .Include("~/Content/vendors/owl.carousel/dist/owl.carousel.js")
                .Include("~/Content/vendors/autosize/dist/autosize.js")
                .Include("~/Content/vendors/clipboard/dist/clipboard.min.js")
                .Include("~/Content/vendors/jquery-validation/dist/jquery.validate.js")
                .Include("~/Content/vendors/bootstrap-notify/bootstrap-notify.min.js")
                .Include("~/Content/vendors/js/framework/components/plugins/base/bootstrap-notify.init.js")
                .Include("~/Content/vendors/toastr/build/toastr.min.js")
                .Include("~/Content/vendors/raphael/raphael.js")
                .Include("~/Content/vendors/morris.js/morris.js")
                .Include("~/Content/vendors/chartist/dist/chartist.js")
                .Include("~/Content/vendors/chart.js/dist/Chart.bundle.js")
                .Include("~/Content/vendors/js/framework/components/plugins/charts/chart.init.js")
                .Include("~/Content/assets/vendors/base/vendors.bundle.js")
                .Include("~/Content/assets/demo/base/scripts.bundle.js")
                .Include("~/Content/assets/vendors/custom/fullcalendar/fullcalendar.bundle.js")
                .Include("~/Content/assets/app/js/dashboard.js")
                .Include("~/Content/assets/snippets/custom/pages/user/login.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"
                      ));

            //bundles.Add(new StyleBundle("~/Admin/vendors/css").Include(
            //    "~/Content/vendors/perfect-scrollbar/css/perfect-scrollbar.css",
            //    "~/Content/vendors/tether/dist/css/tether.css",
            //    "~/Content/vendors/bootstrap-datepicker/dist/css/bootstrap-datepicker3.min.css",
            //    "~/Content/vendors/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.css",
            //    "~/Content/vendors/bootstrap-switch/dist/css/bootstrap3/bootstrap-switch.css",
            //    "~/Content/vendors/bootstrap-select/dist/css/bootstrap-select.css",
            //    "~/Content/vendors/owl.carousel/dist/assets/owl.carousel.css",
            //    "~/Content/vendors/animate.css/animate.css",
            //    "~/Content/vendors/toastr/build/toastr.css",
            //    "~/Content/vendors/jstree/dist/themes/default/style.css",
            //    "~/Content/vendors/morris.js/morris.css",
            //    "~/Content/vendors/chartist/dist/chartist.min.css",
            //    "~/Content/vendors/socicon/css/socicon.css",
            //    "~/Content/vendors/vendors/flaticon/css/flaticon.css",
            //    "~/Content/vendors/vendors/metronic/css/styles.css",
            //    "~/Content/vendors/vendors/fontawesome5/css/all.min.css",
            //    "~/Content/assets/vendors/base/vendors.bundle.css",
            //    "~/Content/assets/demo/base/style.bundle.css",
            //    "~/Content/assets/vendors/custom/fullcalendar/fullcalendar.bundle.css"
            //    ));

            bundles.Add(new StyleBundle("~/Admin/vendors/css")
                .Include("~/Content/vendors/perfect-scrollbar/css/perfect-scrollbar.css")
                .Include("~/Content/vendors/tether/dist/css/tether.css")
                .Include("~/Content/vendors/bootstrap-datepicker/dist/css/bootstrap-datepicker3.min.css")
                .Include("~/Content/vendors/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.css")
                .Include("~/Content/vendors/bootstrap-switch/dist/css/bootstrap3/bootstrap-switch.css")
                .Include("~/Content/vendors/bootstrap-select/dist/css/bootstrap-select.css")
                .Include("~/Content/vendors/owl.carousel/dist/assets/owl.carousel.css")
                .Include("~/Content/vendors/animate.css/animate.css")
                .Include("~/Content/vendors/toastr/build/toastr.css")
                .Include("~/Content/vendors/jstree/dist/themes/default/style.css")
                .Include("~/Content/vendors/morris.js/morris.css")
                .Include("~/Content/vendors/chartist/dist/chartist.min.css")
                .Include("~/Content/vendors/socicon/css/socicon.css")
                .Include("~/Content/vendors/vendors/flaticon/css/flaticon.css")
                .Include("~/Content/vendors/vendors/metronic/css/styles.css")
                .Include("~/Content/vendors/vendors/fontawesome5/css/all.min.css")
                .Include("~/Content/assets/vendors/base/vendors.bundle.css")
                .Include("~/Content/assets/demo/base/style.bundle.css")
                .Include("~/Content/assets/vendors/custom/fullcalendar/fullcalendar.bundle.css")

          );

            BundleTable.EnableOptimizations = false;

        }
    }
}
