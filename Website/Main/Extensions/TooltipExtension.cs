﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Web;

namespace Main.Extensions
{
    public static class TooltipExtension
    {
        public static IHtmlContent Tooltip(this IHtmlHelper helper, string tooltip, string direction = "top")
        {
            return new HtmlString(String.Format("<span class='sp-popover' data-bs-trigger='hover focus' data-bs-placement='{1}' data-bs-title='{0}'><img src='/que.png' style='width: 16px; height: 16px;'></span>", tooltip, direction));
        }

    }

}
