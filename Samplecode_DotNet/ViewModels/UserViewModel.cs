
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Samplecode_DotNet.Ops;
using Samplecode_DotNet.Models;

namespace Samplecode_DotNet.ViewModels
{
    public class UserViewModel
    {
        public LoginModel loginModel { get; set; }
        public LoginModel Login(LoginModel loginModel)
        {
            return UserOps.Login(loginModel);
        }
    }
}