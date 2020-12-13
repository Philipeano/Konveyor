using Konveyor.Core.Models;
using System.Linq;

namespace Konveyor.Core.ViewModels
{
    public class OrderDetailViewModel
    {
        public OrderDetailViewModel(Orders order, IQueryable<OrderUpdates> orderUpdates, IQueryable<Packages> packageList)
        {
            Order = order;
            OrderUpdates = orderUpdates;
            Packages = packageList;
        }

        public Orders Order { get; set; }

        public IQueryable<OrderUpdates> OrderUpdates { get; set; }

        public IQueryable<Packages> Packages { get; set; }
    }
}
