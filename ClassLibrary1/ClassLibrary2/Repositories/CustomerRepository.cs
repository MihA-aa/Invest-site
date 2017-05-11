using DAL.Entities;
using DAL.Interfaces;
using DALEF.EF;

namespace DALEF.Repositories
{
    class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationContext context) : base(context)
        {
        }

        public void Delete(string id)
        {
            Customer item = dbSet.Find(id);
            if (item != null)
                dbSet.Remove(item);
        }
        public Customer Get(string id)
        {
            return dbSet.Find(id);
        }
    }
}
