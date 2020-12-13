using System;
using System.Collections.Generic;

namespace Konveyor.Core.Models
{
    public partial class Packages
    {
        public Packages()
        {
            PackageUpdates = new HashSet<PackageUpdates>();
        }

        public long PackageId { get; set; }
        public int PackageTypeId { get; set; }
        public string Description { get; set; }
        public long OrderId { get; set; }
        public bool Fragile { get; set; }
        public double? Weight { get; set; }
        public double? Volume { get; set; }

        public virtual Orders Order { get; set; }
        public virtual PackageTypes PackageType { get; set; }
        public virtual ICollection<PackageUpdates> PackageUpdates { get; set; }
    }
}
