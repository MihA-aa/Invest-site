using DAL.Entities;
using DALEF.EF;
using DAL.Interfaces;
namespace DALEF.Repositories
{
    public class PositionRepository : GenericRepository<Position>, IPositionRepository
    {
        public PositionRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
