using System;
using System.Collections.Generic;

namespace Konveyor.Core.Models
{
    public partial class CostParameters
    {
        public int ParameterId { get; set; }
        public string ParameterName { get; set; }
        public string Description { get; set; }
        public string Scope { get; set; }
        public decimal? Value { get; set; }
        public string Operand { get; set; }
        public double? Multiplier { get; set; }
        public bool? IsActive { get; set; }
    }
}
