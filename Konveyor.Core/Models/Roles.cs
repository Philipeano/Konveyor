using System;
using System.Collections.Generic;

namespace Konveyor.Data
{
    public partial class Roles
    {
        public Roles()
        {
            Employees = new HashSet<Employees>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
