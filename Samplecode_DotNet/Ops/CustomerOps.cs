using System;
using System.Collections.Generic;
using System.Linq;

using System.Data.Entity.Core.Objects;
using System.IO;
using Samplecode_DotNet.Helper;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Text;
using System.Net.Mime;
using System.Net.Mail;
using System.Net;
using System.Web;
using Samplecode_DotNet.ViewModels;
using Samplecode_DotNet.Models;
using Samplecode_DotNet.DAL;


namespace Samplecode_DotNet.OPs
{
    public class CustomerOps
    {
        public static CustomerViewModel GetCustomerList()
        {
            CustomerViewModel model = new CustomerViewModel();
            model.objModel = new CustomerModel();
           
            int? UserId = 1;
            try
            {
                using (SampleCodeEntities db = new SampleCodeEntities())
                {
                    model.objCustomerListModels = db.Customers.Where(x=>UserId== UserId).Select(x => new CustomerModel
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Emailaddress = x.Emailaddress, 
                        CompanyName=x.CompanyName,
                        Country=x.Country,
                        ContactNo=x.ContactNo,
                        ResendMsg = "",
                        CustomerId=x.Id,
                        URL=x.URL,
                        ContactPerson=x.ContactPerson,
                        Designation=x.Designation
                    }
                     ).ToList();
                    model.objCategoryList = db.Categories.Select(y => new CategoryModel
                    {

                        Id = y.Id,
                        Name = y.Name

                    }).ToList();
                  
                    return model;
                   
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }
        public static CustomerViewModel GetCustomerSummaryList(int CustomerId)
        {
            CustomerViewModel model = new CustomerViewModel();
            model.objModel = new CustomerModel();            
            try
            {     
                //Code for return summary list
                 return model;

             
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }
        public static CustomerViewModel ImportCustomer(string FilePath)
        {
            CustomerViewModel model = new CustomerViewModel();
            DataTable dt = new DataTable();
            dt = ProcessCSV(FilePath);
            using (SampleCodeEntities db = new SampleCodeEntities())
            {               
                var parameter = new SqlParameter("@ImportCustomerType", SqlDbType.Structured);
                parameter.TypeName = "dbo.BulkImportCustomerType";
                parameter.Value = dt;

                db.Database.ExecuteSqlCommand("exec dbo.SP_ImportCustomer @ImportCustomerType", parameter);
            }

            return model;
        }
        private static DataTable ProcessCSV(string fileName)
        {
            string Feedback = string.Empty;
            string line = string.Empty;
            string[] strArray;
            DataTable dt = new DataTable();
            DataRow row;
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            StreamReader sr = new StreamReader(fileName);
            line = sr.ReadLine();
            strArray = r.Split(line);
            Array.ForEach(strArray, s => dt.Columns.Add(new DataColumn()));
            while ((line = sr.ReadLine()) != null)
            {
                row = dt.NewRow();
                row.ItemArray = r.Split(line);
                dt.Rows.Add(row);
            }
            sr.Dispose();
            return dt;
        }
        public static CustomerViewModel GetCategoryList()
        {
            CustomerViewModel model = new CustomerViewModel();
            try
            {
                using (SampleCodeEntities db = new SampleCodeEntities())
                {
                    model.objCategoryList = db.Categories.Select(y => new CategoryModel 
                    {

                        Id = y.Id,
                        Name = y.Name

                    }).ToList();
                    return model;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }
        public static CustomerViewModel SendEmail(string Custoemrid,string CategoryId,string TemplateId)
        {
            bool IsSend = false;
            string ErrorCode = "", ErrorMessage = "";

            CustomerViewModel model = new CustomerViewModel();
            try
            {
                int UserId = Helper.SessionHelper.GetCurrentUserID();
                int intCategoryId = Convert.ToInt32(CategoryId);
                int intTemplateId = Convert.ToInt32(TemplateId);
                string[] CustomerIdList = Custoemrid.Split(',');
                if (CustomerIdList.Length > 0)
                {
                    for (int i = 0; i <= CustomerIdList.Length-1; i++)
                    {
                        using (SampleCodeEntities db = new SampleCodeEntities())
                        {
                            if (CustomerIdList[i].Length > 0)
                            {
                                int CustomerId =Convert.ToInt32(CustomerIdList[i]);
                                var userresult = db.UserRegistrations.FirstOrDefault(x => x.UserId == UserId);

                                string TemplateURL = "";
                                string Attachment = "~/Template/Portfolio.pdf";
                                var result = db.Customers.FirstOrDefault(x => x.Id == CustomerId);
                                if (result != null)
                                {

                                    string Emailaddress = result.Emailaddress;
                                    string CustomerName = result.FirstName + " " + result.LastName;
                                    SendEmailTemplate(Emailaddress, result.FirstName, result.LastName,result.CompanyName, userresult.InitialName,
                                                      userresult.FirstName, userresult.LastName, TemplateURL, Attachment, ref IsSend,ref ErrorCode,ref ErrorMessage);
                                    if (IsSend == true)
                                    {
                                        //Action when email send
                                    }
                                }
                                   
                                
                                
                            }
                            if (IsSend == false)
                            {
                                model.ErrorCode = ErrorCode;
                                model.ErrorMessage = ErrorMessage;
                            }
                            else
                            {
                                model.ErrorCode = ErrorCode;
                                model.ErrorMessage = ErrorMessage;
                            }

                        }
                           
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }
        private static void SendEmailTemplate(string EmailAddress,string FirstName,string LastName,string CompanyName,string IntialName,
                                                string UserFirstName,string UserLastName,  string TemplateURL,string attachmentFilename,
                                                ref bool IsSend,ref string ErrorCode,ref string ErrorMessage)
        {
            IsSend = false;
            //Attachment attachment = new Attachment(attachmentFilename, MediaTypeNames.Application.Octet);
            try
            {    
                var userName = System.Configuration.ConfigurationManager.AppSettings["GmailUserName"].ToString();
                var userPassword = System.Configuration.ConfigurationManager.AppSettings["GmailPassword"].ToString();
               
                if (IntialName.Length <= 0)
                {
                    IntialName = "IMENSO";
                }
                var fromMail = new System.Net.Mail.MailAddress(userName, IntialName);
                var toMail = new MailAddress(EmailAddress);
                string subject = "---Subject----";
                string body = CreateEmailBody(FirstName, LastName, CompanyName, UserFirstName, UserLastName, TemplateURL); 
                using (MailMessage mail = new MailMessage(fromMail, toMail))
                {
                    mail.Subject = subject;
                    mail.Body = body;
                    string path = HttpContext.Current.Server.MapPath(attachmentFilename);
                    mail.Attachments.Add(new Attachment(path));
                    mail.Priority = System.Net.Mail.MailPriority.High;
                    mail.IsBodyHtml = true;
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;                    
                    mail.Headers.Add("Disposition-Notification-To", fromMail.ToString());

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(userName, userPassword),

                    };
                    smtp.Send(mail);
                };


                //CreateEmailBody(FirstName, LastName, CompanyName, UserFirstName, UserLastName, TemplateURL);


                IsSend = true;
                ErrorCode = "Success";
                ErrorMessage = "Email Sent Successfully.";
            }

            catch (Exception ex)
            {                
                IsSend = false;
                ErrorCode = "Error";
                ErrorMessage = ex.Message.ToString();
            }

        }
        private static string CreateEmailBody(string FirstName,string LastName,string CompanyName,string UserFirstName,string UserLastName, string TemplateURL)
        {
            try
            {

                string body = string.Empty;
                using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(TemplateURL)))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{FirstName}", FirstName);
               
                return body;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return "";
            }
        }
    }
}