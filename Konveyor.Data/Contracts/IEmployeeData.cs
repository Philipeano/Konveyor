using Konveyor.Core.ViewModels;

namespace Konveyor.Data.Contracts
{
    public interface IEmployeeData
    {

        public EmployeeListViewModel GetAllEmployees();

        public EmployeeDetailViewModel GetEmployeeDetails(long employeeId);

        public EmployeeEditViewModel GetEmployeeForEdit(long? employeeId);

        public bool RemoveEmployee(long? employeeId);

        public bool SaveEmployeeToDb(EmployeeEditViewModel employeeInfo);
    }
}
