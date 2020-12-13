using System;
using System.Collections.Generic;

namespace Konveyor.Core.Models
{
    public partial class Users
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? LastLoginDate { get; set; }

        public virtual Customers Customers { get; set; }
        public virtual Employees Employees { get; set; }
    }
}
