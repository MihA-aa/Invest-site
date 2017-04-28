using ClassLibrary1.Entities;
using ClassLibrary1.Interfaces;
using ClassLibrary2.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.Repositories
{
    class CustomerRepository : IRepository<Customer>
    {
        ApplicationContext db;
        public CustomerRepository(ApplicationContext context)
        {
            this.db = context;
        }
        public void Create(Customer customer)
        {
            db.Customers.Add(customer);
        }

        public void Delete(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer != null)
                db.Customers.Remove(customer);
        }

        public IEnumerable<Customer> Find(Func<Customer, bool> predicate)
        {
            return db.Customers.Where(predicate).ToList();
        }

        public Customer Get(int id)
        {
            return db.Customers.Find(id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return db.Customers;
        }

        public void Update(Customer portfolio)
        {
            db.Entry(portfolio).State = EntityState.Modified;
        }
    }
}
