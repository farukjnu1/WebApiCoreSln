using System;
using System.Collections.Generic;

namespace WebApiCore.DataModels.EntityModels.TestDB
{
    public partial class SecurityMenu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int? ModuleId { get; set; }
        public string Controller { get; set; }
    }
}
