using System;
using System.Collections.Generic;

namespace Konveyor.Core.Models
{
    public class Offices
    {
        public Offices()
        {
            OrdersDestinationOffice = new HashSet<Orders>();
            OrdersOriginOffice = new HashSet<Orders>();
        }

        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public bool? IsActive { get; set; }

        public virtual NigerianStates State { get; set; }
        public virtual ICollection<Orders> OrdersDestinationOffice { get; set; }
        public virtual ICollection<Orders> OrdersOriginOffice { get; set; }
    }
}
