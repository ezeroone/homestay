using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;

namespace coffeelovesmilk.Infrastructure.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString HtmlLink(this HtmlHelper html, string url, string text, object htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("a") { InnerHtml = text };
            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tagBuilder.MergeAttribute("href", url);
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }

        public static string UrlCleanString(this string urlToEncode)
        {
           urlToEncode = string.IsNullOrWhiteSpace(urlToEncode) ? string.Empty : urlToEncode.Trim().ToLower();

           //remove invalid characters
           urlToEncode = Regex.Replace(urlToEncode, "[^a-z0-9]", "-");

           //convert multiple dashes into one
           urlToEncode = Regex.Replace(urlToEncode, @"-+", "-").Trim();

           //return url.ToString();
           return urlToEncode;
        }

        public static string EmptyActionLink(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            var requestContext = HttpContext.Current.Request.RequestContext;
            string str = UrlHelper.GenerateUrl(null, actionName, controllerName, null, null, null, new RouteValueDictionary(routeValues), htmlHelper.RouteCollection, requestContext, true);
            TagBuilder builder2 = new TagBuilder("a")
            {
                InnerHtml = string.Empty
            };
            TagBuilder builder = builder2;
            builder.MergeAttributes<string, object>(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            builder.MergeAttribute("href", str);
            return builder.ToString(TagRenderMode.Normal);
        }
    }
}