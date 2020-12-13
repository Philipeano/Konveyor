using Konveyor.Core.Models;
using System.Collections.Generic;

namespace Konveyor.Core.ViewModels
{
    public class OfficeEditViewModel
    {
        public OfficeEditViewModel(Offices office, List<NigerianStates> nigerianStates)
        {
            Office = office;
            NigerianStates = nigerianStates;
        }

        public Offices Office { get; set; }

        public List<NigerianStates> NigerianStates { get; set; }
    }
}
