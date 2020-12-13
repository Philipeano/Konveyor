using Konveyor.Core.Models;
using System.Linq;

namespace Konveyor.Core.ViewModels
{
    public class PackageDetailViewModel
    {
        public PackageDetailViewModel(Packages package, IQueryable<PackageUpdates> packageUpdates)
        {
            Package = package;
            PackageUpdates = packageUpdates;
        }

        public Packages Package { get; set; }

        public IQueryable<PackageUpdates> PackageUpdates { get; set; }
    }
}
