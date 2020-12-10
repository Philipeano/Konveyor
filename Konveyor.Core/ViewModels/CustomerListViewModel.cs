using Konveyor.Core.Models;
using System.Collections.Generic;

namespace Konveyor.Core.ViewModels
{
    class CustomerListViewModel
    {        
        public CustomerListViewModel(List<Customers> customerList)
        {
            // Important: Assign only customers with 'IsActive = True'
            ActiveCustomers = customerList;
        }

        public List<Customers> ActiveCustomers { get; set; }
    }
}
