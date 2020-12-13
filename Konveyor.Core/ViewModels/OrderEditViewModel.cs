using Konveyor.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Konveyor.Core.ViewModels
{
    public class OrderEditViewModel
    {
        public OrderEditViewModel(Orders order, IQueryable<OrderUpdates> orderUpdates, IQueryable<Packages> packageList)
        {
            Order = order;
            OrderUpdates = orderUpdates;
            Packages = packageList;
        }

        public Orders Order { get; set; }

        public IQueryable<OrderUpdates> OrderUpdates { get; set; }

        public IQueryable<Packages> Packages { get; set; }

        public List<Customers> CustomerList { get; set; }

        public List<Offices> OfficeList { get; set; }

        public List<OrderStatus> OrderStatusOptions { get; set; }
    }
}
