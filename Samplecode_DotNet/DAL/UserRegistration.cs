//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Samplecode_DotNet.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserRegistration
    {
        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<System.DateTime> DateofBirth { get; set; }
        public string Password { get; set; }
        public string InitialName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string IPAddress { get; set; }
        public string UserType { get; set; }
    }
}