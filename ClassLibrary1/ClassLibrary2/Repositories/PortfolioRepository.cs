using DAL.Entities;
using DALEF.EF;

namespace DALEF.Repositories
{
    public class PortfolioRepository : GenericRepository<Portfolio>
    {
        public PortfolioRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
