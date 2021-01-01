using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Konveyor.Core.ViewModels
{
    public class PackageEditViewModel : PackageBaseViewModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Select the type of package.")]
        [Display(Name = "Package Type")]
        public List<SelectListItem> PackageTypeOptions { get; set; }

        /* 
           IMPORTANT NOTES:         

           When creating a new 'Packages' record:
           - 'Recorder' should provide 'LoggedBy' value in 'PackageUpdates'
           -  When Authentication/Authorisation is implemented, the logged-in user will automatically be the Recorder.

           When updating an 'Packages' record: 
           - 'Updater' should provide subsequent 'LoggedBy' value in 'PackageUpdates' 
           -  When Authentication/Authorisation is implemented, the logged-in user will automatically be the Updater.
           - 'NewStatus' should provide subsequent 'NewPackageStatus' value in 'PackageUpdates'                  
         */

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the staff member who received this package from the sender.")]
        [Display(Name = "Recorded By")]
        public List<SelectListItem> RecorderOptions { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the staff member updating this package.")]
        [Display(Name = "Updated By")]
        public List<SelectListItem> UpdaterOptions { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Specify the new status of this package.")]
        [Display(Name = "New Status")]
        public List<SelectListItem> NewStatusOptions { get; set; }
    }
}
