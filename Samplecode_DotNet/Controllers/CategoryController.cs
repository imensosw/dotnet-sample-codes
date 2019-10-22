using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Samplecode_DotNet.ViewModels;

namespace Samplecode_DotNet.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            if (Helper.SessionHelper.GetCurrentUserID() == 0)
            {
                return RedirectToAction("Registration", "Account");
            }
            CategoryViewModel model = new CategoryViewModel();
            model = model.GetCategoryList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CategoryList", model);

            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddCategory(CategoryViewModel model)
        {
            if (Helper.SessionHelper.GetCurrentUserID() == 0)
            {
                return RedirectToAction("Registration", "Account");
            }
            try
            {               
                if (ModelState.IsValid)
                {
                    if (model.objModel.Name.Length > 0 && model.objModel.Name != null)
                    {
                        model = model.AddCategory(model);
                        var isAjax = Request.IsAjaxRequest();
                        if (isAjax && model.ErrorCode == "Error")
                        {
                            return PartialView("_AddCategory", model);
                        }
                    }
                }
                return PartialView("_AddCategory", model);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Content("An error occurred");
            }
        }
        [Route("Delete/{Id}")]
        public ActionResult DeleteCategory(int Id)
        {
            if (Helper.SessionHelper.GetCurrentUserID() == 0)
            {
                return RedirectToAction("Registration", "Account");
            }
            CategoryViewModel model = new CategoryViewModel();
            model.DeleteCategory(Id);
           return RedirectToAction("Index");
            
        }
    }
}