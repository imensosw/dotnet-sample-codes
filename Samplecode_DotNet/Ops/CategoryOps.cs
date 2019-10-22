using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Samplecode_DotNet.ViewModels;
using Samplecode_DotNet.Models;
using Samplecode_DotNet.DAL;


namespace Samplecode_DotNet.Ops
{
    public class CategoryOps
    {
        //Get Category list
        public static CategoryViewModel GetCategoryList()
        {
            CategoryViewModel model = new CategoryViewModel();           
            model.objModel = new CategoryModel();
           
            try
            {
                using (SampleCodeEntities db = new SampleCodeEntities())
                {
                    //Bind 
                    model.objCategoryListModels = db.Categories.Select(x => new CategoryModel
                    {
                        Name = x.Name,
                        Type = "Type",
                        TypeId = x.TypeId,
                        Rownumber = 0,
                        Id = x.Id
                    }
                     ).ToList();                  
                    return model;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }       
        private static bool IsCheckExistsCategory(string Name, string Type)
        {
            try
            {
                using (SampleCodeEntities db = new SampleCodeEntities())
                {
                    return db.Categories.Any(x => x.Name.ToUpper() == Name.ToUpper());
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }
        public static CategoryViewModel AddCategory(CategoryViewModel model)
        {
            try
            {
                using (SampleCodeEntities db = new SampleCodeEntities())
                {
                    if (!IsCheckExistsCategory(model.objModel.Name, model.objModel.Type))
                    {
                        Category C = new Category
                        {
                            Name = model.objModel.Name,
                            TypeId = model.objModel.TypeId
                        };

                        db.Categories.Add(C);
                        if (db.SaveChanges() > 0)
                        {

                            model.ErrorCode = "Success";
                            model.ErrorMessage = "Category created successfully.";
                        }
                        else
                        {
                            model.ErrorCode = "Error";
                            model.ErrorMessage = "Name and Type already Exists!";
                        }
                      
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                model.ErrorCode = "Error";
                model.ErrorMessage = ex.Message;
            }
            return model;
        }
        public static CategoryViewModel DeleteCategory(int id)
        {
            CategoryViewModel model = new CategoryViewModel();
            try
            {
                using (SampleCodeEntities db = new SampleCodeEntities())
                {
                    Category CT = db.Categories.Where(x => x.Id == id).FirstOrDefault();
                    db.Categories.Remove(CT);
                    if (db.SaveChanges() > 0)
                    {
                        model.ErrorCode = "success";
                        model.ErrorMessage = "Category deleted successfully.";
                    }
                    else
                    {
                        model.ErrorCode = "Error";
                        model.ErrorMessage = "Error";
                    }
                }
               
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return model;
        }
    }
}