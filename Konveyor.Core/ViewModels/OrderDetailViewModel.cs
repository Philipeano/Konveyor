using System.ComponentModel.DataAnnotations;

namespace Konveyor.Core.ViewModels
{
    public class OrderDetailViewModel
    {
        public long OrderId { get; set; }

        [Display(Name = "Tracking Code")]
        public string TrackingCode { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter recipient's full name."), MaxLength(100)]
        [Display(Name = "Recipient's Full Name")]
        public string RecipientName { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Enter recipient's phone/mobile number."), MaxLength(20)]
        [RegularExpression(@"(^[0-9]{9}$)|(^0[7-9]{1}[0-9]{9}$)|(^\+234[7-9]{1}[0-9]{9}$)|(^\+234[0-9]{9}$)", ErrorMessage = "Phone number is not valid.")]
        [Display(Name = "Recipient's Phone Number")]
        public string RecipientPhone { get; set; }


        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Enter the recipient's contact or residential address."), MaxLength(300)]
        [Display(Name = "Recipient's Address")]
        public string RecipientAddress { get; set; }


        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Provide a brief summary of, or comment on, this delivery order."), MaxLength(500)]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }


        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Enter the total cost of this delivery order.")]
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

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Select the customer placing this delivery order.")]
        [Display(Name = "Sender")]
        public string SenderName { get; set; }


        public int OriginOfficeId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Select the location the package(s) are being sent from.")]
        [Display(Name = "Origin")]
        public string OriginOfficeName { get; set; }


        public int DestinationOfficeId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Select the location the package(s) are being sent to.")]
        [Display(Name = "Destination")]
        public string DestinationOfficeName { get; set; }


        /*
        IMPORTANT NOTES: 
        
        The fields below are retrieved from the 'OrderUpdates' records meant for this 'Orders' record:
        - The 'DateInitiated' and 'Initiator' fields are populated by 'EntryDate' and 'ProcessedBy' (VERY FIRST RECORD)
        - The 'Remarks' and 'CurrentStatus' fields are populated by 'Remarks' and 'NewOrderStatus' (MOST RECENT RECORD)
        */

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Specify the date this delivery order was placed.")]
        [Display(Name = "Order Date")]
        public System.DateTime DateInitiated { get; set; }


        public long InitiatorId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the attendant / staff member who initiated this delivery order.")]
        [Display(Name = "Initiated By")]
        public string InitiatorName { get; set; }


        public int CurrentStatusId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the current status of this delivery order.")]
        [Display(Name = "Status")]
        public string CurrentStatus { get; set; }
    }
}



//[DataType(DataType.Text)]
//[Required(ErrorMessage = "Enter .")]
//[Display(Name = "")]
