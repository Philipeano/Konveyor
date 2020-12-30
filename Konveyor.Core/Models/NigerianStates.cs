using System;
using System.Collections.Generic;

namespace Konveyor.Core.Models
{
    public class NigerianStates
    {
        public NigerianStates()
        {
            Offices = new HashSet<Offices>();
        }

        public int StateId { get; set; }
        public string State { get; set; }
        public string Capital { get; set; }
        public string Abbreviation { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Offices> Offices { get; set; }
    }
}
