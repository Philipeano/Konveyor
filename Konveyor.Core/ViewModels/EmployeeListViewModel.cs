using Konveyor.Core.Models;
using System.Linq;

namespace Konveyor.Core.ViewModels
{
    public class EmployeeListViewModel
    {
        public EmployeeListViewModel(IQueryable<Employees> employeeList)
        {
            // Important: Assign only employees with 'IsActive = True'
            ActiveEmployees = employeeList;
        }

        public IQueryable<Employees> ActiveEmployees { get; set; }
    }
}
