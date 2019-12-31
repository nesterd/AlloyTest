using System;
using System.Collections.Generic;
using System.Text;

namespace AlloyTestApp.Core.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string City { get; set; }
        public decimal Amount { get; set; }
    }
}
