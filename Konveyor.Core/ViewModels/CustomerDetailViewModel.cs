using Konveyor.Core.Models;

namespace Konveyor.Core.ViewModels
{
    public class CustomerDetailViewModel
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
    }

}
