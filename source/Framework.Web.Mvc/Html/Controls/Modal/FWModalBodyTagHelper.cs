﻿using System;
using System.Collections.Generic;
using System.Text;
using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework modal tag helper.
    /// </summary>
    [HtmlTargetElement("modalbody")]
    public class FWModalBodyTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            var modal = context.Items["modal"] as FWModalControl;
            modal.Body = ChildContent.GetContent();
            return modal;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWModalBodyTagHelper" />.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        public FWModalBodyTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor) 
            : base(urlHelperFactory, actionAccessor)
        { }
    }
}
