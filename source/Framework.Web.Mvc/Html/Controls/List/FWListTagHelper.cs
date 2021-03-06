﻿using Framework.Web.Mvc.Exceptions;
using Framework.Web.Mvc.Html.Controls;
using Framework.Web.Mvc.Html.Controls.List;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO;
using System.Reflection;

namespace Framework.Web.Mvc.Html
{
    /// <summary>
    /// Framework list tag helper.
    /// </summary>
    [HtmlTargetElement("list")]
    [RestrictChildren("listtemplate")]
    public class FWListTagHelper : FWControlTagHelper
    {
        /// <summary>
        /// Gets or sets the service name to load the data.
        /// </summary>
        [HtmlAttributeName("asp-service")]
        public Type Service { get; set; }

        /// <summary>
        /// Gets or sets the method name to load the data.
        /// </summary>
        [HtmlAttributeName("asp-method")]
        public string Method { get; set; }

        /// <summary>
        /// Creates the framework control.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>The control instance.</returns>
        protected override IFWHtmlElement RenderControl(TagHelperContext context, TagHelperOutput output)
        {
            if (Id == null)
            {
                Id = TagBuilder.CreateSanitizedId(Path.GetFileNameWithoutExtension(ViewContext.View.Path), "_");
            }

            FWListOptions options = CreateComponentConfig();

            // Gets the metadata from the return type of the service method.
            var metadata = RequestContext.MetadataProvider.GetMetadataForType(options.ServiceMethod.ReturnParameter.ParameterType);
            var control = new FWListControl(RequestContext, metadata, options);
            control.Attributes.Add("data-control", "list");

            context.Items["TemplateOptions"] = options;
            ChildContent.GetContent();

            return control;
        }

        private FWListOptions CreateComponentConfig()
        {
            // Configures the grid method service.
            var service = ServiceProvider.GetService(Service);
            if (service == null)
            {
                throw new FWImplementationNotFoundException(Service);
            }

            var options = new FWListOptions($"{ViewContext.ViewData.ModelMetadata.ModelType.Name}_{Id}")
            {
                Id = Id,
                ServiceType = Service,
                ServiceMethod = Service.GetMethod(Method),
                ViewPath = ViewContext.View.Path
            };

            // Caches the options for 2 hours.
            MemoryCache.Set(options.CacheKey, options, new TimeSpan(2, 0, 0));

            return options;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWGridTagHelper" />.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="urlHelperFactory">The url helper factory.</param>
        /// <param name="actionAccessor">The action accessor.</param>
        /// <param name="metadataProvider">The model metadata provider.</param>
        /// <param name="memoryCache">The memory cache object reference.</param>
        public FWListTagHelper(IServiceProvider serviceProvider, IUrlHelperFactory urlHelperFactory,
                                IActionContextAccessor actionAccessor, IModelMetadataProvider metadataProvider,
                                IMemoryCache memoryCache) 
            : base(urlHelperFactory, actionAccessor, metadataProvider)
        {
            ServiceProvider = serviceProvider;
            MemoryCache = memoryCache;
        }

        private IServiceProvider ServiceProvider { get; set; }

        private IMemoryCache MemoryCache { get; set; }

        /// <summary>
        /// Gets or sets the view context.
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
    }
}
