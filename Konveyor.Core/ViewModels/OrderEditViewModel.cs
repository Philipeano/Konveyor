using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Konveyor.Core.ViewModels
{
    public class OrderEditViewModel : OrderBaseViewModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Select the customer placing this order.")]
        [Display(Name = "Sender")]
        public List<SelectListItem> SenderOptions { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Select the location the package(s) are being sent from.")]
        [Display(Name = "Origin")]
        public List<SelectListItem> OriginOptions { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Select the location the package(s) are being sent to.")]
        [Display(Name = "Destination")]
        public List<SelectListItem> DestinationOptions { get; set; }

        /* 
           IMPORTANT NOTES:         

           When creating a new 'Orders' record:
           - 'Initiator' should provide 'ProcessedBy' value in 'OrderUpdates'
           -  When Authentication/Authorisation is implemented, the logged-in user will automatically be the Initiator.

           When updating an 'Orders' record: 
           - 'Updater' should provide subsequent 'ProcessedBy' value in 'OrderUpdates' 
           -  When Authentication/Authorisation is implemented, the logged-in user will automatically be the Updater.
           - 'NewStatus' should provide subsequent 'NewOrderStatus' value in 'OrderUpdates'                  
         */

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the staff member initiating this order.")]
        [Display(Name = "Initiated By")]
        public List<SelectListItem> InitiatorOptions { get; set; }

        
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the staff member updating this order.")]
        [Display(Name = "Updated By")]
        public List<SelectListItem> UpdaterOptions { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the new status of this order.")]
        [Display(Name = "New Status")]
        public List<SelectListItem> NewStatusOptions { get; set; }
    }
}