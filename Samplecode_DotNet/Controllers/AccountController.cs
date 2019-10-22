using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Samplecode_DotNet.Models;
using Samplecode_DotNet.ViewModels;

namespace Samplecode_DotNet.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
     
        public ActionResult Index()
        {
            if (Helper.SessionHelper.GetCurrentUserID() == 0)
            {
                return RedirectToAction("Registration", "Account");
            }

            return View();
        }
       //Registration Action
        
        public ActionResult Registration()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            
            UserViewModel model = new UserViewModel();
            model.loginModel = new LoginModel();
            return View(model);
        }
        //Login Post Action
        [HttpPost]
       
        public ActionResult Login(UserViewModel model)
        {
            Session.Clear();
            var isAjax = Request.IsAjaxRequest();
            if (isAjax)
            {
                model.loginModel = model.Login(model.loginModel);
                if (!string.IsNullOrWhiteSpace(model.loginModel.UserName) && model.loginModel.UserId > 0)
                {
                    Session["UserSession"] = model.loginModel.userSession;
                }
                else
                {
                    Session["UserSession"] = new UserSession();
                }
            }
            return PartialView("_Login", model);
        }
       
    }
}