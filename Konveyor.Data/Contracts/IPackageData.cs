using Konveyor.Core.ViewModels;

namespace Konveyor.Data.Contracts
{
    public interface IPackageData
    {

        public PackageListViewModel GetAllPackages();

        public PackageDetailViewModel GetPackageDetails(long packageId);

        public PackageEditViewModel GetPackageForEdit(long? packageId);

        public bool RemovePackage(long? packageId);

        public bool SavePackageToDb(PackageEditViewModel packageInfo);
    }
}
