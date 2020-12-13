using Konveyor.Core.Models;
using System.Linq;

namespace Konveyor.Core.ViewModels
{
    public class PackageListViewModel
    {
        public PackageListViewModel(IQueryable<Packages> packages)
        {
            Packages = packages;
        }

        public IQueryable<Packages> Packages { get; set; }
    }
}
