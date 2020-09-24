using System;
using System.Collections.Generic;
using System.Text;

namespace design_pattern_repository.Domain.Entities
{
    public class Planet: AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool HasOxygen { get; set; }
        public long Diameter { get; set; }

    }
}
