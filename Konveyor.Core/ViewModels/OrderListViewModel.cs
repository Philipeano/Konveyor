using Konveyor.Core.Models;
using System.Linq;

namespace Konveyor.Core.ViewModels
{
    public class OrderListViewModel
    {
        public OrderListViewModel(IQueryable<Orders> orderList)
        {
            Orders = orderList;
        }

        public IQueryable<Orders> Orders { get; set; }
    }
}
