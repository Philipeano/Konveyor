using System;
using System.Collections.Generic;

namespace Konveyor.Data
{
    public partial class Orders
    {
        public Orders()
        {
            OrderUpdates = new HashSet<OrderUpdates>();
            Packages = new HashSet<Packages>();
        }

        public long OrderId { get; set; }
        public string TrackingCode { get; set; }
        public long SenderId { get; set; }
        public int OriginOfficeId { get; set; }
        public int DestinationOfficeId { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhone { get; set; }
        public string RecipientAddress { get; set; }
        public string Remarks { get; set; }
        public decimal TotalCost { get; set; }
        public bool ExpressService { get; set; }
        public double? ExpectedNumOfDays { get; set; }

        public virtual Offices DestinationOffice { get; set; }
        public virtual Offices OriginOffice { get; set; }
        public virtual Customers Sender { get; set; }
        public virtual ICollection<OrderUpdates> OrderUpdates { get; set; }
        public virtual ICollection<Packages> Packages { get; set; }
    }
}
