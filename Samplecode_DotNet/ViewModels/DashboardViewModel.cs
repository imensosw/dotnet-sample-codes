using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Samplecode_DotNet.Models;
using Samplecode_DotNet.Ops;

namespace Samplecode_DotNet.ViewModels
{
    public class DashboardViewModel
    {
        public DashboardModel objModel { get; set; }
        public List<DashboardModel> DashboardListModels { get; set; }
        public List<DashboardModel> DashboardTypeListModels { get; set; }
        public List<DashboardModel> CustomerSummarytListModels { get; set; }            
        public List<DashboardModel> objDashboardList { get; set; }
        public string FinalErrorMessage { get; set; }
        public string FinalErrorFlag { get; set; }

        public DashboardViewModel GetDashboardList(int id)
        {
            return DashboardOps.GetDashboardList(id);
        }
        

    }
}