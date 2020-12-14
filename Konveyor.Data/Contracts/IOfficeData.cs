using Konveyor.Core.ViewModels;

namespace Konveyor.Data.Contracts
{
    public interface IOfficeData
    {

        public OfficeListViewModel GetAllOffices();

        public OfficeDetailViewModel GetOfficeDetails(long officeId);

        public OfficeEditViewModel GetOfficeForEdit(long? officeId);

        public bool RemoveOffice(long? officeId);

        public bool SaveOfficeToDB(OfficeEditViewModel officeInfo);
    }
}
