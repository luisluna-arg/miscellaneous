public static class ScriptsLoader
{
    private static class Scripts
    {
        public static Dictionary<string, string[]> Urls = new Dictionary<string, string[]>() {
            {
                "~/bundles/angularJS",
                new [] {
                    "~/Scripts/app/libs/angular.js",
                    "~/Scripts/app/libs/angular-cookies-1.0.0rc10.js",
                    "~/Scripts/app/libs/localize/localize.js",
                    "~/Scripts/app/libs/plugin/checklist-model.js",
                    "~/Scripts/app/libs/route/angular-animate.js",
                    "~/Scripts/app/libs/route/angular-route-segment.js",
                    "~/Scripts/app/libs/route/angular-route.js",
                    "~/Scripts/app/libs/route/view-segment.js"
                }
            }
        };
    }

    public static System.Web.IHtmlString Render(this UrlHelper helper, string bundleName)
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder("");

        if (Scripts.Urls.ContainsKey(bundleName))
        {
            string[] items = Scripts.Urls[bundleName];

            for (int i = 0; i < items.Length; i++)
            {
                builder.Append("<script type=\"text/javascript\" src=\"").Append(helper.Content(items[i])).Append("\" ></script>\n");
            }
        }

        return new System.Web.HtmlString(builder.ToString());
    }

}
