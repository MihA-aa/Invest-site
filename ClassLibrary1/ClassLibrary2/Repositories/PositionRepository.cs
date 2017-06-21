using System.Linq;
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
        public bool IsExist(int id)
        {
            return dbSet
                .AsNoTracking()
                .Any(p => p.Id == id);
        }

        public IQueryable<Position> GetPositionsQuery()
        {
           return dbSet.AsNoTracking();
        }

    }
}
