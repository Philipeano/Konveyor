using Konveyor.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Konveyor.Core.ViewModels
{
    public class PackageEditViewModel
    {
        public PackageEditViewModel(Packages package, IQueryable<PackageUpdates> packageUpdates)
        {
            Package = package;
            PackageUpdates = packageUpdates;
        }

        public Packages Package { get; set; }

        public IQueryable<PackageUpdates> PackageUpdates { get; set; }

        public List<PackageTypes> PackageTypeOptions { get; set; }

        public List<PackageStatus> PackageStatusOptions { get; set; }
    }
}
