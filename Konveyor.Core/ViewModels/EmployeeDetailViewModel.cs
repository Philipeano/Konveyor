using Konveyor.Core.Models;

namespace Konveyor.Core.ViewModels
{
    public class EmployeeDetailViewModel
    {
        public EmployeeDetailViewModel(Users user, Employees employee)
        {
            User = user;
            Employee = employee;
        }

        public Users User { get; set; }

        public Employees Employee { get; set; }
    }
}
