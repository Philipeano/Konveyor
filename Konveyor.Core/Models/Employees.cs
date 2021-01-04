using System;
using System.Collections.Generic;

namespace Konveyor.Core.Models
{
    public class Employees
    {
        public Employees()
        {
            OrderUpdates = new HashSet<OrderUpdates>();
            PackageUpdates = new HashSet<PackageUpdates>();
        }

        public long EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string Designation { get; set; }
        public long? UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool? IsActive { get; set; }
        public string Status { get; set; }

        public virtual Roles Role { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<OrderUpdates> OrderUpdates { get; set; }
        public virtual ICollection<PackageUpdates> PackageUpdates { get; set; }
    }
}
