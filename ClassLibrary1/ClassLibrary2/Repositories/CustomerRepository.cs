using DAL.Entities;
using DALEF.EF;

namespace DALEF.Repositories
{
    class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
