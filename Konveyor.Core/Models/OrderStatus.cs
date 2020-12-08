using System;
using System.Collections.Generic;

namespace Konveyor.Data
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            OrderUpdates = new HashSet<OrderUpdates>();
        }

        public int OrderStatusId { get; set; }
        public string OrderStatus1 { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<OrderUpdates> OrderUpdates { get; set; }
    }
}
