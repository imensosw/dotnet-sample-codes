using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Samplecode_DotNet.ViewModels;
using Samplecode_DotNet.DAL;
using Samplecode_DotNet.Models;

namespace Samplecode_DotNet.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        [Route("CustomerList")]
        public ActionResult CustomerList()
        {
            if (Helper.SessionHelper.GetCurrentUserID() == 0)
            {
                return RedirectToAction("Registration", "Account");
            }
            CustomerViewModel model = new CustomerViewModel();
            model = model.GetCustomerList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CustomerList", model);

            }
            return View(model);

        }
        [HttpPost]
        public ActionResult AddCustomer(CustomerViewModel model)
        {
            
            DataTable dt = new DataTable();
            if (Helper.SessionHelper.GetCurrentUserID() == 0)
            {
                return RedirectToAction("Registration", "Account");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Fileupload != null)
                    {
                        var supportedTypes = new[] { "csv" };
                        var fileExt = System.IO.Path.GetExtension(model.Fileupload.FileName).Substring(1);
                        if (!supportedTypes.Contains(fileExt))
                        {
                            model.ErrorCode = "Error";
                           model.ErrorMessage = "File Extension Is InValid - Only Upload CSV File";
                           
                        }                       
                        else
                        {                          
                            string fileName = Path.GetFileName(model.Fileupload.FileName);
                            int fileSize = model.Fileupload.ContentLength;
                            int Size = fileSize / 1000000;
                            string filetype = model.Fileupload.ContentType;
                            model.Fileupload.SaveAs(Server.MapPath("~/CustomerFileUpload/" + fileName));
                            string path = Server.MapPath("~/CustomerFileUpload/" + fileName);                           
                            model.ImportCustomer(path);
                        }
                        
                    }
                }
                if (model.ErrorCode == "Error")
                {
                    TempData["ErrorMessage"] = model.ErrorMessage;
                    TempData["ErrorCode"] = model.ErrorCode;
                }
                else
                {
                    TempData["ErrorMessage"] = "File Import Successfully";
                    TempData["ErrorCode"] = "Success";
                }
                return RedirectToAction("CustomerList");

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Content("An error occurred");
            }
        }

       
        public ActionResult Edit(int Id)
        {
            CustomerViewModel model = new CustomerViewModel();
            using (SampleCodeEntities db = new SampleCodeEntities())
            {
                model.objModel = db.Customers.Where(z=> z.Id==Id).Select(x => new CustomerModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Emailaddress = x.Emailaddress,
                    CompanyName = x.CompanyName,
                    Country = x.Country,
                    ContactNo = x.ContactNo,
                    CustomerId = x.Id,
                    URL = x.URL,
                    ContactPerson = x.ContactPerson,
                    Designation = x.Designation
                }).FirstOrDefault();
            }

            return PartialView("_EditCustomer", model);
        }
        [HttpPost]
        public ActionResult Update(CustomerViewModel model)
        {
           // CustomerViewModel model = new CustomerViewModel();
            using (SampleCodeEntities db = new SampleCodeEntities())
            {
                var custinfo = db.Customers.Where(x => x.Id == model.objModel.CustomerId).FirstOrDefault();
                custinfo.FirstName = model.objModel.FirstName;
                custinfo.LastName = model.objModel.LastName;
                custinfo.Emailaddress = model.objModel.Emailaddress;
                custinfo.CompanyName = model.objModel.CompanyName;
                custinfo.Country = model.objModel.Country;
                custinfo.ContactNo = model.objModel.ContactNo;
                custinfo.URL = model.objModel.URL;
                custinfo.ContactPerson = model.objModel.ContactPerson;
                custinfo.Designation = model.objModel.Designation;

                if (db.SaveChanges() > 0)
                {
                    return RedirectToAction("CustomerList");
                }
            }

            return View( model);
        }       
      
        public FileResult DownloadSample()
        {
            string path = Server.MapPath("~/CustomerFileUpload/Sample.CSV");
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = "Sample.CSV";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        
       

        }
}