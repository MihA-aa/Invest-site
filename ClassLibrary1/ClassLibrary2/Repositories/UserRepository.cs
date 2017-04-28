using ClassLibrary1.Interfaces;
using ClassLibrary2.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ClassLibrary1;

namespace ClassLibrary2.Repositories
{
    class UserRepository : IRepository<User>
    {
        ApplicationContext db;
        public UserRepository(ApplicationContext context)
        {
            this.db = context;
        }
        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }
    }
}
