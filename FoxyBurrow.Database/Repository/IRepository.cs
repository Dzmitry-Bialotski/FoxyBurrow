using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoxyBurrow.Database.Repository
{
    public interface IRepository<T>
    {
        T Get(long id);
        IQueryable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
}
