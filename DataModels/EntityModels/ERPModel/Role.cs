using System;
using System.Collections.Generic;

namespace DataModels.EntityModels.ERPModel
{
    public partial class Role
    {
        public Role()
        {
            User = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<User> User { get; set; }
    }
}
