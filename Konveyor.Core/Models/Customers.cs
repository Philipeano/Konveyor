using System;
using System.Collections.Generic;

namespace Konveyor.Data
{
    public partial class Customers
    {
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }

        public long CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string PreferredName { get; set; }
        public string ContactAddress { get; set; }
        public long? UserId { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool? IsActive { get; set; }
        public string Status { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
