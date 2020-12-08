using System;
using System.Collections.Generic;

namespace Konveyor.Data
{
    public partial class PackageStatus
    {
        public PackageStatus()
        {
            PackageUpdates = new HashSet<PackageUpdates>();
        }

        public int PackageStatusId { get; set; }
        public string PackageStatus1 { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<PackageUpdates> PackageUpdates { get; set; }
    }
}
