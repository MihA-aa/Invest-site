using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;
using DALEF.EF;
using System.Threading.Tasks;

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

        public async Task<T> GetAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList(); ;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync(); ;
        }

        public void Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
