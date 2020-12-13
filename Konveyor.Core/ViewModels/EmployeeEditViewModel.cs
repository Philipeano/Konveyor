using Konveyor.Core.Models;
using System.Collections.Generic;

namespace Konveyor.Core.ViewModels
{
    class EmployeeEditViewModel
    {
        public EmployeeEditViewModel(Users user, Employees employee, List<string> genderOptions)
        {
            User = user;
            Employee = employee;
            GenderOptions = genderOptions;
        }

        public Users User { get; set; }

        public Employees Employee { get; set; }

        public List<string> GenderOptions { get; set; }
    }
}
