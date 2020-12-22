using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Konveyor.Core.ViewModels
{
    public class OfficeEditViewModel
    {
        public int OfficeId { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter a name for this office/branch."), MaxLength(200)]
        [Display(Name = "Office/Branch Name")]
        public string OfficeName { get; set; }


        [DataType(DataType.PhoneNumber)]
        // [Required(ErrorMessage = "Enter a phone number (if available) for this office/branch"), MaxLength(20)]
        [RegularExpression(@"(^[0-9]{9}$)|(^0[7-9]{1}[0-9]{9}$)|(^\+234[7-9]{1}[0-9]{9}$)|(^\+234[0-9]{9}$)", ErrorMessage = "Phone number is not valid.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        [DataType(DataType.EmailAddress)]
        // [Required(ErrorMessage = "Enter a phone number (if available) for this office/branch."), MaxLength(150)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email address is not valid.")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }


        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Provide the physical address of this office/branch."), MaxLength(300)]
        [Display(Name = "Office Address")]
        public string Address { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the town or city where this office is located."), MaxLength(50)]
        [Display(Name = "City/Town")]
        public string City { get; set; }


        public int StateId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Select the state where this office is located."), MaxLength(50)]
        [Display(Name = "State")]
        public List<SelectListItem> StateOptions { get; set; }
    }
}
