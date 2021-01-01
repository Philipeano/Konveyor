using System.ComponentModel.DataAnnotations;
using Konveyor.Core.ViewModels;

namespace Konveyor.Core.ViewModels
{
    public class PackageDetailViewModel : PackageBaseViewModel
    {
        [Display(Name = "Package Type")]
        public string PackageType { get; set; }


        [Display(Name = "Recorded By")]
        public string RecorderName { get; set; }


        [Display(Name = "Status")]
        public string CurrentStatus { get; set; }
    }
}
