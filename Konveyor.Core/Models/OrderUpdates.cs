using System;
using System.Collections.Generic;

namespace Konveyor.Data
{
    public partial class OrderUpdates
    {
        public long EntryId { get; set; }
        public long OrderId { get; set; }
        public int NewOrderStatusId { get; set; }
        public string Remarks { get; set; }
        public long? ProcessedBy { get; set; }
        public DateTime EntryDate { get; set; }

        public virtual OrderStatus NewOrderStatus { get; set; }
        public virtual Orders Order { get; set; }
        public virtual Employees ProcessedByNavigation { get; set; }
    }
}
