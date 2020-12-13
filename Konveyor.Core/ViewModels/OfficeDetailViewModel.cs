using Konveyor.Core.Models;

namespace Konveyor.Core.ViewModels
{
    public class OfficeDetailViewModel
    {
        public OfficeDetailViewModel(Offices office)
        {
            Office = office;
        }

        public Offices Office { get; set; }
    }
}
