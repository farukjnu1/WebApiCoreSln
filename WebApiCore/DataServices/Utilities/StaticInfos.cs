using DataModels.ViewModels;
using System;
using System.Collections.Generic;

namespace DataUtilities
{
    public static class StaticInfos
    {
        public const String conString = @"Server=.;Database=Admission;User Id=sa; Password=123;";
        public const String GlobalDateFormat = "MM/dd/yyyy";
        public const int CompanyID = 1;
        public const int LoggedUserID = 1;
        
        // rgl_secret_api_key
        public const String userAgent = "User-Agent";
        public const String authToken = "authToken";

        public const int TokenExpiry = 30; // minutes
        
        //Mail Type
        public enum Mail
        {
            UserRegistrationMail = 1,
            VmDeploymentMail = 2,
            AdditionalServiceMAil = 3,
            NoCCmail = 4
        }

        public const string SalesMailAccount = "sales@nodi.com";
        public const string CloudMailAccount = "cloud@nodi.com";

        #region time zone
        public const string LinuxTimeZoneBD = "Asia/Dhaka";
        public const string TimeZoneBD = "Bangladesh Standard Time";
        public const string IspDateFormat = "dd MMM yyyy HH:mm:ss";
        #endregion

    }
}
