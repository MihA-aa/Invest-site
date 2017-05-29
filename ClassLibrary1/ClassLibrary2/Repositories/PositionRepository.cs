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
        public bool CheckIfPositionExists(int id)
        {
            return dbSet
                .AsNoTracking()
                .Any(p => p.Id == id);
        }
    }
}
