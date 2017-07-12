using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;
//using DALEF.EF;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Enums;
using NHibernate;
using NHibernate.Event.Default;
using NHibernate.Linq;

namespace DALEF.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        //protected ApplicationContext db;
        //protected DbSet<T> dbSet;
        protected readonly ISession Session;

        public GenericRepository(ISession session)
        {
            Session = session;
        }

        public void Create(T item)
        {
            Session.Save(item);
        }

        public void Delete(int id)
        {
            Session.Delete(Session.Load<T>(id));
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return Session
            .Query<T>()
            .Where(predicate);
        }
        
        public T Get(int id)
        {
            return Session.Get<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Session.Query<T>();
        }

        public void Update(T item)
        {
            Session.Update(item);
        }

        public int Count()
        {
            return Session
            .Query<T>()
            .Count();
        }
    }
}
