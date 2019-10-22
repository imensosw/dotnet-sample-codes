using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samplecode_DotNet.Models
{
    public class DashboardModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Emailaddress { get; set; }
        public string Country { get; set; }
        public string ContactNo { get; set; }
        public string TypeName { get; set; }
        public string SendEmail { get; set; }
        public int? TypeCount { get; set; }
        public string BackgroundColor { get; set; }
        public string ForeColor { get; set; }
        public string CustomerName { get; set; }
        public string TemplateName { get; set; }
        public DateTime SendDatetime { get; set; }
        public string TemplatePath { get; set; }
        public int? CustomerId { get; set; }
    }
}