using Konveyor.Core.Models;
using System.Collections.Generic;

namespace Konveyor.Core.ViewModels
{
    class EmployeeListViewModel
    {
        public EmployeeListViewModel(List<Employees> employeeList)
        {
            // Important: Assign only employees with 'IsActive = True'
            ActiveEmployees = employeeList;
        }

        public List<Employees> ActiveEmployees { get; set; }
    }
}
