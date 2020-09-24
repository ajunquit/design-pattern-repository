using System;
using System.Collections.Generic;
using System.Text;

namespace design_pattern_repository.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IPlanetRepository Planets { get; }
        IStarRepository Stars { get; }
        int SaveChanges();
    }
}
