using ClassLibrary1.EF;
using ClassLibrary1.Entities;
using ClassLibrary1.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Repositories
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
