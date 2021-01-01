using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Konveyor.Core.ViewModels
{
    public class OfficeEditViewModel : OfficeBaseViewModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Select the state where this office is located.")]
        [Display(Name = "State")]
        public List<SelectListItem> StateOptions { get; set; }
    }
}
