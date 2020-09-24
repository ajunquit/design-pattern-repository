﻿using design_pattern_repository.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace design_pattern_repository.Domain.Interfaces
{
    public interface IPlanetRepository: IRepository<Planet>
    {
        IEnumerable<Planet> GetPlanetsHasOxygen();
    }
}
