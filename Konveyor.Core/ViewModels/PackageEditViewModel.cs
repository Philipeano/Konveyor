using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Konveyor.Core.ViewModels
{
    public class PackageEditViewModel
    {
        public long PackageId { get; set; }

        public int PackageTypeId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Select the type of package."), MaxLength(50)]
        [Display(Name = "Package Type")]
        public List<SelectListItem> PackageTypeOptions { get; set; }


        [DataType(DataType.MultilineText), MaxLength(500)]
        // [Required(ErrorMessage = "Enter a brief description of the package. (Optional)")]
        [Display(Name = "Description")]
        public string Description { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Indicate if the item is fragile or requires special handling.")]
        [Display(Name = "Fragile")]
        public bool Fragile { get; set; }


        [DataType(DataType.Text), Range(0, 100)]
        [Required(ErrorMessage = "Enter the weight of the item in kilograms.")]
        [Display(Name = "Weight (kg)")]
        public double Weight { get; set; }


        [DataType(DataType.Text), Range(0, 5)]
        [Required(ErrorMessage = "Enter the volume of the item in cubic metres.")]
        [Display(Name = "Volume (m3)")]
        public double Volume { get; set; }


        public long OrderId { get; set; }

        [DataType(DataType.Custom)]
        [Display(Name = "Order Details")]
        public OrderDetailViewModel OrderVM { get; set; }

        /*
            IMPORTANT: When creating a new 'Packages' record:
            - The 'Remarks' field should provide values for both 'Packages' and 'PackageUpdates'
            - The 'DateRecorded' field should provide 'EntryDate' value in 'PackageUpdates'
            - The 'Recorder' fields should provide 'LoggedBy' value in 'PackageUpdates'
            - The 'CurrentStatus' fields should provide 'NewPackageStatus' value of '0 - Logged' in 'PackageUpdates' 
        */

        [DataType(DataType.MultilineText), MaxLength(500)]
        // [Required(ErrorMessage = "Provide a brief summary of, or comment on, this package."), MaxLength(500)]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Specify the date this package was recieved from the sender.")]
        [Display(Name = "Date Recorded")]
        public System.DateTime DateRecorded { get; set; }


        // NOTE: When Authentication/Authorisation is implemented, the logged-in user will automatically be the Recorder.
        public long RecorderId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the staff member who received this package from the sender.")]
        [Display(Name = "Recorded By")]
        public List<SelectListItem> RecorderOptions { get; set; }


        public int CurrentStatusId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the current status of this package.")]
        [Display(Name = "Status")]
        public string CurrentStatus { get; set; }


        /*
            IMPORTANT: When updating a 'Packages' record: 
            - The 'NewRemarks' field should provide subsequent 'Remarks' value in 'PackageUpdates'
            - The 'DateUpdated' field should provide subsequent 'EntryDate' value in 'PackageUpdates'
            - The 'Updater' fields should provide subsequent 'LoggedBy' value in 'PackageUpdates' 
            - The 'NewStatus' fields should provide subsequent 'NewPackageStatus' value in 'PackageUpdates'
        */

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Comment briefly on the update being made to this package."), MaxLength(500)]
        [Display(Name = "New Remarks")]
        public string NewRemarks { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Specify the date this package is being updated.")]
        [Display(Name = "Last Updated")]
        public System.DateTime DateUpdated { get; set; }


        // NOTE: When Authentication/Authorisation is implemented, the logged-in user will automatically be the Updater.
        public long UpdaterId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the staff member updating this package.")]
        [Display(Name = "Updated By")]
        public List<SelectListItem> UpdaterOptions { get; set; }


        public int NewStatusId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the new status of this package.")]
        [Display(Name = "New Status")]
        public List<SelectListItem> NewStatusOptions { get; set; }
    }
}
