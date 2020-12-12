using Konveyor.Core.Models;

namespace Konveyor.Core.ViewModels
{
    class EmployeeDetailViewModel
    {
        public EmployeeDetailViewModel(Employees employee, Users user)
        {
            Employee = employee;
            User = user;
        }

        public Employees Employee { get; set; }

        public Users User { get; set; }
    }
}
