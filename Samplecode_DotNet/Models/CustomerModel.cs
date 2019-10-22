using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Samplecode_DotNet.Models
{
    public class CustomerModel
    {
        public int? CustomerId { get; set; }
        [Required(ErrorMessage = "Please First name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Last name.")]
        public string LastName { get; set; }
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Please enter Email address")]
        public string Emailaddress { get; set; }
        [Required(ErrorMessage = "Please Company Name.")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Please Country.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Please Contact No.")]
        public string ContactNo { get; set; }
        public string ResendMsg { get; set; }
        public long? Rownumber { get; set; }
        [Required(ErrorMessage = "Please URL.")]
        public string URL { get; set; }
        [Required(ErrorMessage = "Please Contact Person.")]
        public string ContactPerson { get; set; }
        [Required(ErrorMessage = "Please Designation.")]
        public string Designation { get; set; }
        public string CustomerName { get; set; }
        public string TemplateName { get; set; }
        public DateTime SendDatetime { get; set; }
        public string TemplatePath { get; set; }

    }
}