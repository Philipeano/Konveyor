using Konveyor.Core.ViewModels;
using System.Collections.Generic;

namespace Konveyor.Data.Contracts
{
    public interface IOfficeData
    {

        public List<OfficeDetailViewModel> GetAllOffices();

        public OfficeDetailViewModel GetOfficeDetails(int officeId);

        public OfficeEditViewModel CreateNewOffice();

        public OfficeEditViewModel GetOfficeForEdit(int officeId);

        public bool TryRemoveOffice(int officeId, out string errorMsg);

        public bool TrySaveOfficeToDB(OfficeEditViewModel officeInfo, out string errorMsg);
    }
}
