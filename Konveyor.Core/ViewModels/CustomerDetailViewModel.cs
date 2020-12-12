using Konveyor.Core.Models;

namespace Konveyor.Core.ViewModels
{
    public class CustomerDetailViewModel
    {
        public CustomerDetailViewModel(Users user, Customers customer)
        {
            User = user;
            Customer = customer;
        }

        public Users User { get; set; }

        public Customers Customer { get; set; }
    }
}
