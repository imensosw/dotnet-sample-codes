using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samplecode_DotNet.Helper
{
    public class SessionHelper
    {
        public static bool GetSessionState()
        {
            bool flag = true;
            if (HttpContext.Current.Session["UserSession"] != null)
            {
                flag = false;
            }
            return flag;
        }

        public static int GetCurrentUserID()
        {
            int currentUserID = 0;
          //return the Logged User use of Session
            return currentUserID;
        }
        public static string GetCurrentUserType()
        {
            string currentUserType = "";
            //return the Logged User type behalf of logged use of Session
            return currentUserType;
        }
        public static string GetInitialName()
        {
            int UserId = GetCurrentUserID();
            string IntialName = "";
           //Get Inital name From Sql Table
            return IntialName;
        }
        public static string GetFullName()
        {
            int UserId = GetCurrentUserID();
            string FullName = "";
           //Return Full name From Sql Table
            return FullName;
        }
    }
}