using System;
using System.Collections.Generic;

namespace WebApiCore.DataModels.EntityModels.TestDB
{
    public partial class SecurityUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserPass { get; set; }
        public int? RoleId { get; set; }
    }
}
