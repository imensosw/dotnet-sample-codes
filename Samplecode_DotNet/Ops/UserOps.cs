
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Samplecode_DotNet.DAL;
using Samplecode_DotNet.Helper;
using Samplecode_DotNet.Models;

namespace Samplecode_DotNet.Ops
{
    public class UserOps
    {

        public static LoginModel Login(LoginModel loginModel)
        {
            try
            {
                using (SampleCodeEntities db = new SampleCodeEntities())
                {
                    var ePassword = CommonHelper.Encrypt_Password_MD5(loginModel.Password);
                    var dp = CommonHelper.Decrypt_Password_MD5(loginModel.Password);
                       var result = db.UserRegistrations.FirstOrDefault(x => x.Status == "Active" && x.EmailAddress == loginModel.EmailAddress && x.Password == ePassword);
                        if (result != null)
                        {
                            loginModel.UserType = result.UserType.Trim();
                            if (loginModel.UserType == "ADMIN")
                            {                               
                                loginModel.UserName = result.EmailAddress;
                                loginModel.UserId = result.UserId;

                                loginModel.userSession = new UserSession();                                
                                loginModel.userSession.UserName = result.EmailAddress;
                                loginModel.userSession.UserID = result.UserId;
                               
                                loginModel.userSession.UserType = result.UserType.Trim();
                                loginModel.ErrorCode = "Success";
                                loginModel.ErrorMessage = "logged in successfully";
                            }
                            else
                            {
                                loginModel.ErrorCode = "Error";
                                loginModel.ErrorMessage = "Login failed username or password is incorrect.";
                            }                      
                        }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                loginModel.ErrorCode = "Error";
                loginModel.ErrorMessage = ex.Message;
            }
            return loginModel;
        }
    }
}