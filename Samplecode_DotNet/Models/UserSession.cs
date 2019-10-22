using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samplecode_DotNet.Models
{
    public class UserSession
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string UserType { get; set; }
    }
}