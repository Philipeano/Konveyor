using System.ComponentModel.DataAnnotations;

namespace Konveyor.Core.ViewModels
{
    public class OrderDetailViewModel : OrderBaseViewModel
    {
        [Display(Name = "Sender")]
        public string SenderName { get; set; }


        [Display(Name = "Origin")]
        public string OriginOfficeName { get; set; }


        [Display(Name = "Destination")]
        public string DestinationOfficeName { get; set; }


        [Display(Name = "Initiated By")]
        public string InitiatorName { get; set; }


        [Display(Name = "Status")]
        public string CurrentStatus { get; set; }
    }
}
