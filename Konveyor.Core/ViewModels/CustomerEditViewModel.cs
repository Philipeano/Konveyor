using Konveyor.Core.Models;
using System.Collections.Generic;

namespace Konveyor.Core.ViewModels
{
    public class CustomerEditViewModel
    {
        public CustomerEditViewModel(Users user, Customers customer, List<string> genderOptions)
        {
            User = user;
            Customer = customer;
            GenderOptions = genderOptions;
        }

        public Users User { get; set; }

        public Customers Customer { get; set; }

        public List<string> GenderOptions { get; set; }
    }
}
