using System.ComponentModel.DataAnnotations;

namespace Konveyor.Core.ViewModels
{
    public abstract class PackageBaseViewModel
    {
        public long PackageId { get; set; }


        public int PackageTypeId { get; set; }


        [DataType(DataType.MultilineText), MaxLength(500)]
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


        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Provide a brief summary of, or comment on, this package."), MaxLength(500)]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }


        public long OrderId { get; set; }

        [DataType(DataType.Custom)]
        [Display(Name = "Order")]
        public OrderDetailViewModel OrderVM { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Specify the date this package was recieved from the sender.")]
        [Display(Name = "Date Recorded")]
        public System.DateTime DateRecorded { get; set; }


        public long RecorderId { get; set; }


        public int CurrentStatusId { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Specify the date this package is being updated.")]
        [Display(Name = "Last Updated")]
        public System.DateTime DateUpdated { get; set; }


        public long UpdaterId { get; set; }


        public int NewStatusId { get; set; }


        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Describe or comment on the update being made to this package."), MaxLength(500)]
        [Display(Name = "New Remarks")]
        public string NewRemarks { get; set; }
    }
}
