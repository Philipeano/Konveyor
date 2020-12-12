using Konveyor.Core.Models;
using System.Collections.Generic;

namespace Konveyor.Core.ViewModels
{
    class EmployeeEditViewModel
    {
        public EmployeeEditViewModel(Employees employee, Users user)
        {
            Employee = employee;
            User = user;
        }

        public Employees Employee { get; set; }

        public Users User { get; set; }

        public List<Roles> RoleOptions { get; set; }
    }
}
