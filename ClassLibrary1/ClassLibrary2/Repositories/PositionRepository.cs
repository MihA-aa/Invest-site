using System.Linq;
using DAL.Entities;
using DAL.Interfaces;
using NHibernate;
using NHibernate.Linq;

namespace DALEF.Repositories
{
    public class PositionRepository : GenericRepository<Position>, IPositionRepository
    {
        public PositionRepository(ISession session) : base(session)
        {
        }
        public bool IsExist(int id)
        {
            return Session.Query<Position>()
                .Any(p => p.Id == id);
        }

        public IQueryable<Position> GetPositionsQuery()
        {
            return Session.Query<Position>();
        }
        public IQueryable<Position> GetPositionQuery(int id)
        {
            return Session.Query<Position>().Where(p => p.Id == id);
        }
    }
}
