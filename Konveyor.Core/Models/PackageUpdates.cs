using System;
using System.Collections.Generic;

namespace Konveyor.Core.Models
{
    public class PackageUpdates
    {
        public long EntryId { get; set; }
        public long PackageId { get; set; }
        public int NewPackageStatusId { get; set; }
        public string Remarks { get; set; }
        public long? LoggedBy { get; set; }
        public DateTime EntryDate { get; set; }

        public virtual Employees LoggedByNavigation { get; set; }
        public virtual PackageStatus NewPackageStatus { get; set; }
        public virtual Packages Package { get; set; }
    }
}
