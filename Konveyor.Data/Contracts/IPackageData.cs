using Konveyor.Core.ViewModels;
using System.Collections.Generic;

namespace Konveyor.Data.Contracts
{
    public interface IPackageData
    {

        public List<PackageDetailViewModel> GetAllPackages(long orderId);

        public PackageDetailViewModel GetPackageDetails(long packageId);

        public PackageEditViewModel CreateNewPackage();

        public PackageEditViewModel GetPackageForEdit(long packageId);

        // public void RemovePackage(long packageId);

        public void SavePackageToDb(PackageEditViewModel packageInfo, out string errorMsg);
    }
}
