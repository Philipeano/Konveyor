using System.ComponentModel.DataAnnotations;

namespace Konveyor.Core.ViewModels
{
    public abstract class OrderBaseViewModel
    {
        public long OrderId { get; set; }


        [Display(Name = "Tracking Code")]
        public string TrackingCode { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter the recipient's full name."), MaxLength(100)]
        [Display(Name = "Recipient's Full Name")]
        public string RecipientName { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Enter the recipient's phone/mobile number."), MaxLength(20)]
        [RegularExpression(@"(^[0-9]{9}$)|(^0[7-9]{1}[0-9]{9}$)|(^\+234[7-9]{1}[0-9]{9}$)|(^\+234[0-9]{9}$)", ErrorMessage = "Phone number is not valid.")]
        [Display(Name = "Recipient's Phone Number")]
        public string RecipientPhone { get; set; }


        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Enter the recipient's contact or residential address."), MaxLength(300)]
        [Display(Name = "Recipient's Address")]
        public string RecipientAddress { get; set; }


        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Provide a brief summary of, or comment on, this order."), MaxLength(500)]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }


        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Enter the total cost of this order.")]
        [Display(Name = "Total Cost (₦)")]
        public decimal TotalCost { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Indicate if this order requires express delivery.")]
        [Display(Name = "Express Service")]
        public bool ExpressService { get; set; }


        [DataType(DataType.Duration)]
        [Required(ErrorMessage = "In how many days is this order expected to be fulfilled?")]
        [Display(Name = "Expected Duration (Days)")]
        public double? ExpectedNumOfDays { get; set; }


        public long SenderId { get; set; }


        public int OriginOfficeId { get; set; }


        public int DestinationOfficeId { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Specify the date this order was placed.")]
        [Display(Name = "Order Date")]
        public System.DateTime DateInitiated { get; set; }


        public long InitiatorId { get; set; }


        public int CurrentStatusId { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Specify the date this order is being updated.")]
        [Display(Name = "Last Updated")]
        public System.DateTime DateUpdated { get; set; }


        public long UpdaterId { get; set; }


        public int NewStatusId { get; set; }


        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Describe or comment on the update being made to this order."), MaxLength(500)]
        [Display(Name = "New Remarks")]
        public string NewRemarks { get; set; }
    }
}
