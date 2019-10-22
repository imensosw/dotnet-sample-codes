using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Samplecode_DotNet.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter e-mail address.")]
        [EmailAddress(ErrorMessage = "E-mail address is not in a valid format.")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please enter password.")]
        public string Password { get; set; }
        public string UserName { get; set; }
        public int? UserId { get; set; }
        public long? PaymentCode { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public UserSession userSession { get; set; }
        public string UserType { get; set; }
    }
}