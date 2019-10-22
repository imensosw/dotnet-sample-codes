using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Samplecode_DotNet.Models;
using Samplecode_DotNet.OPs;


namespace Samplecode_DotNet.ViewModels
{
    public class CustomerViewModel
    {
        public CustomerModel objModel { get; set; }
        public HttpPostedFileBase Fileupload { get; set; }
        public List<CustomerModel> objCustomerListModels { get; set; }
        public List<CustomerModel> CustomerSummarytListModels { get; set; }
        public string FinalErrorMessage { get; set; }
        public string FinalErrorFlag { get; set; }
        public string SearchText { get; set; }
        public string SearchCharcter { get; set; }
        public List<CustomerModel> objCustomerList { get; set; }
        public List<CategoryModel> objCategoryList { get; set; }        
        public decimal filesize { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public CustomerViewModel GetCustomerList()
        {
            return CustomerOps.GetCustomerList();
        }
        public CustomerViewModel GetCustomerSummaryList(int CustomerId)
        {
            return CustomerOps.GetCustomerSummaryList(CustomerId);
        }
        public CustomerViewModel ImportCustomer(string FilePath)
        {
            return CustomerOps.ImportCustomer(FilePath);
        }
        public CustomerViewModel GetCategoryList()
        {
            return CustomerOps.GetCategoryList();
        }
      
        public CustomerViewModel SemdEmail(string CustomerId,string CategoryId,string TemplateId)
        {
            return CustomerOps.SendEmail(CustomerId, CategoryId, TemplateId);
        }
    }
   
}