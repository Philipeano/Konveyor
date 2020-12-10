using System;
using System.Collections.Generic;

namespace Konveyor.Core.Models
{
    public partial class PackageTypes
    {
        public PackageTypes()
        {
            Packages = new HashSet<Packages>();
        }

        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Packages> Packages { get; set; }
    }
}
