using ClassLibrary1.EF;
using ClassLibrary1.Entities;
using ClassLibrary1.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Repositories
{
    public class PositionRepository : IRepository<Position>
    {
        ApplicationContext db;
        public PositionRepository(ApplicationContext context)
        {
            this.db = context;
        }
        public void Create(Position position)
        {
            db.Positions.Add(position);
        }

        public void Delete(int id)
        {
            Position position = db.Positions.Find(id);
            if (position != null)
                db.Positions.Remove(position);
        }

        public IEnumerable<Position> Find(Func<Position, bool> predicate)
        {
            return db.Positions.Where(predicate).ToList();
        }

        public Position Get(int id)
        {
            return db.Positions.Find(id);
        }

        public IEnumerable<Position> GetAll()
        {
            return db.Positions;
        }

        public void Update(Position position)
        {
            db.Entry(position).State = EntityState.Modified;
        }
    }
}
