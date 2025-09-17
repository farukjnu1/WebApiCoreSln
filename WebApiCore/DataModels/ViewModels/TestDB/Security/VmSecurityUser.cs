using System;
using System.Collections.Generic;
using System.Text;
using WebApiCore.DataModels.EntityModels.TestDB;

namespace DataModels.ViewModels
{
    public class VmSecurityUser : SecurityUser
    {
        public string RoleName { get; set; }
        public string LoginTime { get; set; }
        public string Token { get; set; }
    }
}
