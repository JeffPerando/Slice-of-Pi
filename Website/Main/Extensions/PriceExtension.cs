
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Main.Extensions
{
    public static class PriceExtension
    {
        public static IHtmlContent Price(this IHtmlHelper helper, int price)
        {
            return new HtmlString(Convert.ToDecimal(price).ToString("C"));
        }

        public static IHtmlContent Price(this IHtmlHelper helper, float price)
        {
            return new HtmlString(Convert.ToDecimal(price).ToString("C"));
        }

        public static IHtmlContent Price(this IHtmlHelper helper, double price)
        {
            return new HtmlString(Convert.ToDecimal(price).ToString("C"));
        }

    }

}
