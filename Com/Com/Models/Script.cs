using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Com.Controllers;

namespace Com.Models
{
    public class Script
    {
        public string MyText { get; set; }
        public string PaperFile { get; set; }
        public HttpPostedFileBase FilePost { get; set; }

    }
}