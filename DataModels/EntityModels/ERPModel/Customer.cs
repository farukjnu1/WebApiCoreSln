using System;
using System.Collections.Generic;

namespace DataModels.EntityModels.ERPModel
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
