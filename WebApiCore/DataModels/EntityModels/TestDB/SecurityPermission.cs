using System;
using System.Collections.Generic;

namespace WebApiCore.DataModels.EntityModels.TestDB
{
    public partial class SecurityPermission
    {
        public int PermissionId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int? ModuleId { get; set; }
        public string Controller { get; set; }
        public int? RoleId { get; set; }
        public bool? IsCreate { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsUpdate { get; set; }
        public bool? IsDelete { get; set; }
    }
}
