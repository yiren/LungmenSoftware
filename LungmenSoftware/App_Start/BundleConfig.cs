﻿using System.Web;
using System.Web.Optimization;

namespace LungmenSoftware
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.11.3.js",
                        "~/Scripts/jquery-ui-1.11.4.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                      "~/Scripts/angular.min.js",
                      "~/Scripts/angular-ui-router.min.js",
                      "~/Scripts/angular-route.min.js",
                      "~/Scripts/angular-animate.min.js",
                      "~/Scripts/ui-grid.min.js"
                        ));

           

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/themes/cupertino/jquery-ui.cupertino.css",
                      "~/Content/themes/base/datepicker.css",
                      "~/Content/themes/base/core.css",
                      "~/Content/site.css", "~/Content/ui-grid.min.css")
                      );

           
            BundleTable.EnableOptimizations = true;
           
        }
    }
}
