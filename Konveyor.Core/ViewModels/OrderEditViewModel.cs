using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Konveyor.Core.ViewModels
{
    public class OrderEditViewModel
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

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Select the customer placing this order.")]
        [Display(Name = "Sender")]
        public List<SelectListItem> SenderOptions { get; set; }


        public int OriginOfficeId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Select the location the package(s) are being sent from.")]
        [Display(Name = "Origin")]
        public List<SelectListItem> OriginOptions { get; set; }


        public int DestinationOfficeId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Select the location the package(s) are being sent to.")]
        [Display(Name = "Destination")]
        public List<SelectListItem> DestinationOptions { get; set; }

        /* 
           IMPORTANT: When creating a new 'Orders' record:
           - The 'Remarks' field should provide values for both 'Orders' and 'OrderUpdates'
           - The 'DateInitiated' field should provide 'EntryDate' value in 'OrderUpdates'
           - The 'Initiator' fields should provide 'ProcessedBy' value in 'OrderUpdates'
           - The 'CurrentStatus' fields should provide 'NewOrderStatus' value of '0 - Initiated' in 'OrderUpdates' 
        */

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Provide a brief summary of, or comment on, this order."), MaxLength(500)]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Specify the date this order was placed.")]
        [Display(Name = "Order Date")]
        public System.DateTime DateInitiated { get; set; }


        // NOTE: When Authentication/Authorisation is implemented, the logged-in user will automatically be the Initiator.
        public long InitiatorId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the staff member initiating this order.")]
        [Display(Name = "Initiated By")]
        public List<SelectListItem> InitiatorOptions { get; set; }


        public int CurrentStatusId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the current status of this order.")]
        [Display(Name = "Current Status")]
        public string CurrentStatus { get; set; }

        /*
           IMPORTANT: When updating an 'Orders' record: 
           - The 'NewRemarks' field should provide subsequent 'Remarks' value in 'OrderUpdates'
           - The 'DateUpdated' field should provide subsequent 'EntryDate' value in 'OrderUpdates'
           - The 'Updater' fields should provide subsequent 'ProcessedBy' value in 'OrderUpdates' 
           - The 'NewStatus' fields should provide subsequent 'NewOrderStatus' value in 'OrderUpdates'
        */

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Describe or comment on the update being made to this order."), MaxLength(500)]
        [Display(Name = "New Remarks")]
        public string NewRemarks { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Specify the date this order is being updated.")]
        [Display(Name = "Last Updated")]
        public System.DateTime DateUpdated { get; set; }


        // NOTE: When Authentication/Authorisation is implemented, the logged-in user will automatically be the Updater.
        public long UpdaterId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the staff member updating this order.")]
        [Display(Name = "Updated By")]
        public List<SelectListItem> UpdaterOptions { get; set; }


        public int NewStatusId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the new status of this order.")]
        [Display(Name = "New Status")]
        public List<SelectListItem> NewStatusOptions { get; set; }
    }
}