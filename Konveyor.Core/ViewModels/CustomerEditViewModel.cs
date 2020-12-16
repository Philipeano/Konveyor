using Konveyor.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Konveyor.Core.ViewModels
{
    public class CustomerEditViewModel
    {
        public long CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string PreferredName { get; set; }
        public string ContactAddress { get; set; }

        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }

        public List<SelectListItem> GenderOptions { get; set; }

    }
}
