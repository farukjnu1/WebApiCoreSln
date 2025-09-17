using System;
using System.Collections.Generic;

namespace DataModels.EntityModels.ERPModel
{
    public partial class Profile
    {
        public int Id { get; set; }
        public int? Age { get; set; }
        public string Name { get; set; }
        public string AboutMe { get; set; }
        public string AboutMyFamily { get; set; }
    }
}
