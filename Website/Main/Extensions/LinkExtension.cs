
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Main.Extensions
{
    public static class LinkExtension
    {
        public static IHtmlContent BrandLink(this IHtmlHelper helper, string brand, string color, string url)
        {
            return new HtmlString($"<div><button type='button' class='btn btn-dark m-1' style='background-color: {color};'><a target='_blank' href='{url}'><img style='vertical-align: middle; display: inline; width: 16px;' src='https://cdn.jsdelivr.net/npm/simple-icons@v6/icons/{brand.ToLower()}.svg' /><span class='text-dark'> {brand}</span></a></button></div>");
        }

    }

}
