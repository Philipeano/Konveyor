using Konveyor.Core.Models;
using System.Linq;

namespace Konveyor.Core.ViewModels
{
    public class CustomerListViewModel
    {        
        public CustomerListViewModel(IQueryable<Customers> customerList)
        {
            // Important: Assign only customers with 'IsActive = True'
            ActiveCustomers = customerList;
        }

        public IQueryable<Customers> ActiveCustomers { get; set; }
    }
}
