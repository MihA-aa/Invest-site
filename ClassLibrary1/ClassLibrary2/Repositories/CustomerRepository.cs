using System.Linq;
using System.Linq.Dynamic;
using DAL.Entities;
using DAL.Interfaces;
using NHibernate;
using NHibernate.Linq;

namespace DALEF.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ISession session) : base(session)
        {
        }

        public Customer GetCustomerByProfileId(string profileId)
        {
            return Session.Query<Customer>().FirstOrDefault(x => x.Profiles.Any(p => p.Id == profileId));
        }

        public bool IsExist(int id)
        {
            return Session.Query<Customer>()
                .Any(p => p.Id == id);
        }

        public void AddProfileToCustomer(Profile profile, int? customerId)
        {
            var customer = Session.Get<Customer>(customerId);
            customer.Profiles.Add(profile);
            profile.Customer = customer;
            profile.CustomerId = customerId;
            Session.Flush();
        }
    }
}
