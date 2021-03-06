﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mvc.DataAnnotations
{
    /// <summary>
    /// Configures a form file upload control.
    /// </summary>
    public class FWUploadAttribute : FWDataAnnotationAttribute
    {
        /// <summary>
        /// Gets or sets the number of maximum files the upload can have.
        /// </summary>
        public int MaxFiles { get; set; } = 1;

        /// <summary>
        /// Gets or sets the allowed mime types.
        /// </summary>
        public string[] AllowedMimes { get; set; }

        /// <summary>
        /// Gets or sets the maximum file size.
        /// </summary>
        public int MaxFileSize { get; set; } = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="FWUploadAttribute"/> class.
        /// </summary>
        public FWUploadAttribute()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FWUploadAttribute"/> class.
        /// </summary>
        /// <param name="allowedMimeTypes">The allowed mime types generated by the framework.</param>
        public FWUploadAttribute(FWUploadMimeTypes allowedMimeTypes)
        {
            switch (allowedMimeTypes)
            {
                case FWUploadMimeTypes.Image:
                    AllowedMimes = new string[] { "image/jpeg", "image/png", "image/gif", "image/bmp" };
                    break;
                case FWUploadMimeTypes.Document:
                    AllowedMimes = new string[] { "text/plain", "application/pdf", "application/rtf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/vnd.oasis.opendocument.text" };
                    break;
            }
        }        
    }

    /// <summary>
    /// Enumerates the 
    /// </summary>
    public enum FWUploadMimeTypes
    {
        /// <summary>
        /// Image mime types.
        /// </summary>
        Image,

        /// <summary>
        /// Document mime types.
        /// </summary>
        Document
    }
}
