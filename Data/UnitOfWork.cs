using design_pattern_repository.Data.Repositories;
using design_pattern_repository.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace design_pattern_repository.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly CosmoContext _context;

        IPlanetRepository _planets;
        IStarRepository _stars;

        public UnitOfWork(CosmoContext context)
        {
            _context = context;
        }

        public IPlanetRepository Planets
        {
            get
            {
                if (_planets == null)
                    _planets = new PlanetRepository(_context);
                return _planets;
            }
        }

        public IStarRepository Stars
        {
            get
            {
                if (_stars == null)
                    _stars = new StarRepository(_context);
                return _stars;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
