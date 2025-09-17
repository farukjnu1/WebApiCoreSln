using System;
using System.Collections.Generic;

namespace DataModels.EntityModels.ERPModel
{
    public partial class Payee
    {
        public int Id { get; set; }
        public string PayeeName { get; set; }
        public string City { get; set; }
        public string TransId { get; set; }
    }
}
