using Konveyor.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Konveyor.Core.ViewModels
{
    public class CustomerListViewModel
    {
        public List<CustomerDetailViewModel> ActiveCustomers { get; set; }
    }

}
