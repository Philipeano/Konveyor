using Konveyor.Core.Models;
using System.Collections.Generic;

namespace Konveyor.Core.ViewModels
{
    class CustomerEditViewModel
    {
        public CustomerEditViewModel(Customers customer, Users user)
        {
            Customer = customer;
            User = user;
        }

        public Customers Customer { get; set; }

        public Users User { get; set; }

        public List<string> GenderOptions = new List<string> {"None selected", "Male", "Female", "Prefer not to say" };
    }
}
