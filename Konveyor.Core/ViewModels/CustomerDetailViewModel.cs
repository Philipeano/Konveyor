using Konveyor.Core.Models;

namespace Konveyor.Core.ViewModels
{
    class CustomerDetailViewModel
    {

        public CustomerDetailViewModel(Customers customer, Users user)
        {
            Customer = customer;
            User = user;
        }

        public Customers Customer { get; set; }

        public Users User { get; set; }
    }
}
