﻿using Framework.Web.Mvc.Html.Controls;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework currency tag helper.
    /// </summary>
    [HtmlTargetElement("currency")]
    public class FWCurrencyTagHelper : FWInputControlTagHelper
    {
        /// <summary>
        /// Gets or sets if the control allows negative values.
        /// </summary>
        [HtmlAttributeName("asp-allownegative")]
        public bool AllowNegative { get; set; }

        /// <summary>
        /// Gets or sets if the control should display the currency symbol.
        /// </summary>
        [HtmlAttributeName("asp-displaycurrency")]
        public bool DisplayCurrency { get; set; }

        /// <summary>
        /// Gets or sets the control icon.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override FWInputControl RenderInputControl(TagHelperContext context, TagHelperOutput output)
        {
            FWCurrencyControl control = new FWCurrencyControl(RequestContext, For.Model, For.Metadata);
            control.Attributes.Add("data-control", "currency");

            control.AllowNegative(AllowNegative);
            control.DisplayCurrency(DisplayCurrency);

            if (HideLabel.HasValue)
                control.HideLabel(HideLabel.Value);

            if (!string.IsNullOrWhiteSpace(Icon))
                control.Icon(Icon);

            return control;
        }

        /// <summary>
        /// Initializes a new instance of the Framework.Web.Mvc.Html.FWTextboxTagHelper class.
        /// </summary>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        /// <param name="modelMetadataProvider">The model metadata provider.</param>
        public FWCurrencyTagHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor, IModelMetadataProvider modelMetadataProvider) 
            : base(urlHelperFactory, actionAccessor, modelMetadataProvider)
        { }
    }
}
