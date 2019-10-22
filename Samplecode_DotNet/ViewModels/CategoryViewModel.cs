using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Samplecode_DotNet.Models;
using Samplecode_DotNet.Ops;

namespace Samplecode_DotNet.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryModel objModel { get; set; }
        public List<CategoryModel> objCategoryListModels { get; set; }       
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string SearchText { get; set; }
        public string SearchCharcter { get; set; }
      //  public List<DashboardModel> objDashCategoryList { get; set; }

        
        public CategoryViewModel GetCategoryList()
        {
            return CategoryOps.GetCategoryList();
        }       
        public CategoryViewModel AddCategory(CategoryViewModel model)
        {
            return CategoryOps.AddCategory(model);
        }
        public CategoryViewModel DeleteCategory(int id)
        {
            return CategoryOps.DeleteCategory(id);
        }
    }
}