using FoxyBurrow.Core.Entity;
using FoxyBurrow.Database.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoxyBurrow.Database.Repositpry
{
    public class EFRepository<T> : IRepository<T> where T : Base
    {
        private readonly EFDbContext _applicationDbContext;
        private readonly DbSet<T> _entities;

        public EFRepository(EFDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _entities = applicationDbContext.Set<T>();
        }
        public T Get(long id)
        {
            return _entities.SingleOrDefault(s => s.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return _entities.AsQueryable();
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
            _applicationDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
            _applicationDbContext.SaveChanges();
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
            _applicationDbContext.SaveChanges();
        }

        public void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
