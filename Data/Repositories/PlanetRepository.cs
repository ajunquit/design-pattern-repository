using design_pattern_repository.Domain.Entities;
using design_pattern_repository.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace design_pattern_repository.Data.Repositories
{
    public class PlanetRepository : Repository<Planet>, IPlanetRepository
    {
        private CosmoContext _cosmoContext => (CosmoContext)_context;

        public PlanetRepository(CosmoContext context) : base(context)
        {
        }
        public IEnumerable<Planet> GetPlanetsHasOxygen()
        {
            return _cosmoContext.Planet.Where(x => x.HasOxygen == true).ToList();
        }
    }
}
