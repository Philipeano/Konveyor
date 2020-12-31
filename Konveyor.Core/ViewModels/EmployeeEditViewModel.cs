using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Konveyor.Core.ViewModels
{
    public class EmployeeEditViewModel : EmployeeBaseViewModel
    {
        [Display(Name = "Gender")]
        public List<SelectListItem> GenderOptions { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Choose a password for signing into your account."), MaxLength(50)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Assign a user role to this employee.")]
        [Display(Name = "User Role")]
        public List<SelectListItem> RoleOptions { get; set; }
    }
}
