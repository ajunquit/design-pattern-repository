using System;
using System.Collections.Generic;
using System.Text;

namespace design_pattern_repository.Domain.Entities
{
    public class Star: AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int TemperatureInGradeCelsius { get; set; }
        public int Weight { get; set; }
        public bool GeneratesEnergy { get; set; }
    }
}
