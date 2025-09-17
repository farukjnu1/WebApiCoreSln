using System;
using System.Collections.Generic;
using System.Text;
using WebApiCore.DataModels.EntityModels.TestDB;

namespace DataModels.ViewModels
{
    public class VmSecurityPermission : SecurityPermission
    {
        public string ModuleName { get; set; }
    }
}
