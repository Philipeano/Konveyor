using Konveyor.Core.ViewModels;
using System.Collections.Generic;

namespace Konveyor.Data.Contracts
{
    public interface IEmployeeData
    {
        public List<EmployeeDetailViewModel> GetAllEmployees();

        public EmployeeDetailViewModel GetEmployeeDetails(long employeeId);

        public EmployeeEditViewModel CreateNewEmployee();

        public EmployeeEditViewModel GetEmployeeForEdit(long employeeId);

        public bool TryRemoveEmployee(long employeeId, out string errorMsg);

        public bool TrySaveEmployeeToDb(EmployeeEditViewModel employeeInfo, out string errorMsg);
    }
}
