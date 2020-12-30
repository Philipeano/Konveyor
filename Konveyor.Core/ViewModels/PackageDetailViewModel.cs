using System.ComponentModel.DataAnnotations;
using Konveyor.Core.ViewModels;

namespace Konveyor.Core.ViewModels
{
    public class PackageDetailViewModel
    {
        public long PackageId { get; set; }

        public int PackageTypeId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Select the type of package."), MaxLength(50)]
        [Display(Name = "Package Type")]
        public string PackageType { get; set; }


        [DataType(DataType.MultilineText), MaxLength(500)]
        // [Required(ErrorMessage = "Enter a brief description of the package. (Optional)")]
        [Display(Name = "Description")]
        public string Description { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Indicate if the item is fragile or requires special handling.")]
        [Display(Name = "Fragile")]
        public bool Fragile { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter the weight of the item in kilograms.")]
        [Display(Name = "Weight (kg)")]
        public double Weight { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter the volume of the item in cubic metres.")]
        [Display(Name = "Volume (m3)")]
        public double Volume { get; set; }


        public long OrderId { get; set; }

        [DataType(DataType.Custom)]
        [Display(Name = "Order")]
        public OrderDetailViewModel OrderVM { get; set; }

        /*
        IMPORTANT NOTES: 
        
            The fields below are retrieved from the 'PackageUpdates' records meant for this 'Packages' record:
            - The 'DateRecorded' and 'Recorder' fields are populated by 'EntryDate' and 'LoggedBy' (VERY FIRST RECORD)
            - The 'Remarks' and 'CurrentStatus' fields are populated by 'Remarks' and 'NewPackageStatus' (MOST RECENT RECORD)
         */

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Specify the date this package was recieved from the sender.")]
        [Display(Name = "Date Recorded")]
        public System.DateTime DateRecorded { get; set; }


        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Provide a brief summary of, or comment on, this package."), MaxLength(500)]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }


        public long RecorderId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the staff member who received this package from the sender.")]
        [Display(Name = "Recorded By")]
        public string RecorderName { get; set; }


        public int CurrentStatusId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the current status of this package.")]
        [Display(Name = "Status")]
        public string CurrentStatus { get; set; }

    }
}
