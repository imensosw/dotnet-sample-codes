using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Samplecode_DotNet.ViewModels;
using Samplecode_DotNet.Models;
using Samplecode_DotNet.DAL;

namespace Samplecode_DotNet.Ops
{
    public class DashboardOps
    {
        public static DashboardViewModel GetDashboardList(int Id)
        {
            DashboardViewModel model = new DashboardViewModel();
            model.objModel = new DashboardModel();
            int UserId = Helper.SessionHelper.GetCurrentUserID();
            try
            {
                using (SampleCodeEntities db = new SampleCodeEntities())
                {
                    model.DashboardListModels = db.usp_s_Dashboard_List(UserId, Id).OrderBy(z=>z.OrderNo).Select(x => new DashboardModel
                    {
                        Name = x.Name,
                        Emailaddress = x.Emailaddress,
                        Country = x.Country,
                        ContactNo = x.ContactNo,
                        TypeName=x.TypeName,
                        SendEmail=x.SendEmail,
                        TypeCount=x.TypeCount,
                        BackgroundColor=x.BackgroundColor,
                        ForeColor=x.ForeColor
                       
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
        
       

    }
}