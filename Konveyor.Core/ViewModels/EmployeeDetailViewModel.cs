using System.ComponentModel.DataAnnotations;

namespace Konveyor.Core.ViewModels
{
    public class EmployeeDetailViewModel : EmployeeBaseViewModel
    {
        public string Gender { get; set; }


        [Display(Name = "User Role")]
        public string RoleName { get; set; }
    }
}
