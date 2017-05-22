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
            return dbSet.AsNoTracking().Any(p => p.Id == id);
        }

        public void ChangePositionParams(Position position)
        {
            dbSet.Attach(position);
            db.Entry(position).Property(x => x.Name).IsModified = true;
            db.Entry(position).Property(x => x.SymbolId).IsModified = true;
            db.Entry(position).Property(x => x.SymbolType).IsModified = true;
            db.Entry(position).Property(x => x.SymbolName).IsModified = true;
            db.Entry(position).Property(x => x.OpenDate).IsModified = true;
            db.Entry(position).Property(x => x.OpenPrice).IsModified = true;
            db.Entry(position).Property(x => x.OpenWeight).IsModified = true;
            db.Entry(position).Property(x => x.TradeType).IsModified = true;
            db.Entry(position).Property(x => x.TradeStatus).IsModified = true;
            db.Entry(position).Property(x => x.Dividends).IsModified = true;
            db.Entry(position).Property(x => x.CloseDate).IsModified = true;
            db.Entry(position).Property(x => x.ClosePrice).IsModified = true;
            db.Entry(position).Property(x => x.CurrentPrice).IsModified = true;
            db.Entry(position).Property(x => x.Gain).IsModified = true;
            db.Entry(position).Property(x => x.AbsoluteGain).IsModified = true;
            db.Entry(position).Property(x => x.MaxGain).IsModified = true;
        }
    }
}
