using System.ComponentModel.DataAnnotations;

namespace Konveyor.Core.ViewModels
{
    public class OfficeDetailViewModel : OfficeBaseViewModel
    {
        [Display(Name = "State")]
        public string State { get; set; }
    }
}
