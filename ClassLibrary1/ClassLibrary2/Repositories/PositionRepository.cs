using DAL.Entities;
using DALEF.EF;

namespace DALEF.Repositories
{
    public class PositionRepository : GenericRepository<Position>
    {
        public PositionRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
