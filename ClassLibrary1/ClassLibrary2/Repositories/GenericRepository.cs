using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;
using DALEF.EF;

namespace DALEF.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected ApplicationContext db;
        protected DbSet<T> dbSet;
        public GenericRepository(ApplicationContext context)
        {
            this.db = context;
            this.dbSet = context.Set<T>();
        }

        public void Create(T item)
        {
            dbSet.Add(item);
        }

        public void Delete(int id)
        {
            T item = dbSet.Find(id);
            if (item != null)
                dbSet.Remove(item);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet;
        }

        public void Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
