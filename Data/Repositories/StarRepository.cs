using design_pattern_repository.Domain.Entities;
using design_pattern_repository.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace design_pattern_repository.Data.Repositories
{
    public class StarRepository: Repository<Star>, IStarRepository
    {
        private CosmoContext _cosmoContext => (CosmoContext)_context;

        public StarRepository(CosmoContext context) : base(context)
        {
        }
    }
}
