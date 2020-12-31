using System.ComponentModel.DataAnnotations;

namespace Konveyor.Core.ViewModels
{
    public abstract class EmployeeBaseViewModel
    {
        public long EmployeeId { get; set; }


        public long UserId { get; set; }


        [Display(Name = "Employee Code")]
        public string EmployeeCode { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter your first name."), MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter your last name."), MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "If you have none, simply use your first name."), MaxLength(50)]
        [Display(Name = "Preferred Name")]
        public string PreferredName { get; set; }


        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Enter your email address."), MaxLength(150)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email address is not valid.")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }


        [DataType(DataType.PhoneNumber, ErrorMessage = "Phone number is not valid.")]
        [Required(ErrorMessage = "Enter your phone/mobile number."), MaxLength(20)]
        [RegularExpression(@"(^[0-9]{9}$)|(^0[7-9]{1}[0-9]{9}$)|(^\+234[7-9]{1}[0-9]{9}$)|(^\+234[0-9]{9}$)", ErrorMessage = "Phone number is not valid.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Enter your contact or residential address."), MaxLength(200)]
        [Display(Name = "Contact Address")]
        public string ContactAddress { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter your designation / job title."), MaxLength(200)]
        [Display(Name = "Designation")]
        public string Designation { get; set; }


        public int RoleId { get; set; }
    }
}
