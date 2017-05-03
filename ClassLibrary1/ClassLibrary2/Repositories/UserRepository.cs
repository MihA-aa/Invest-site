using DAL.Entities;
using DALEF.EF;

namespace DALEF.Repositories
{
    class UserRepository : GenericRepository<User>
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
