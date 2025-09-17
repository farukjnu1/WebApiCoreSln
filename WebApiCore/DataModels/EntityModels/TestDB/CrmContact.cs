using System;
using System.Collections.Generic;

namespace WebApiCore.DataModels.EntityModels.TestDB
{
    public partial class CrmContact
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
    }
}
