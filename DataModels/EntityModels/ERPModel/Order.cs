using System;
using System.Collections.Generic;

namespace DataModels.EntityModels.ERPModel
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public string Description { get; set; }
        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
