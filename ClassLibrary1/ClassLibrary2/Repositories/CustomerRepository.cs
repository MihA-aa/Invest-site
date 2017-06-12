using System.Linq;
using System.Linq.Dynamic;
using DAL.Entities;
using DAL.Interfaces;
using DALEF.EF;

namespace DALEF.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationContext context) : base(context)
        {
        }

        public Customer GetCustomerByProfileId(string profileId)
        {
            return dbSet.FirstOrDefault(x => x.Profiles.Any(p => p.Id == profileId));
        }

        public bool IsExist(int id)
        {
            return dbSet
                .AsNoTracking()
                .Any(p => p.Id == id);
        }
    }
}
