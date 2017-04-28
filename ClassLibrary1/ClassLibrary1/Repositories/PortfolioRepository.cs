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
    class PortfolioRepository : IRepository<Portfolio>
    {
        ApplicationContext db;
        public PortfolioRepository(ApplicationContext context)
        {
            this.db = context;
        }
        public void Create(Portfolio portfolio)
        {
            db.Portfolios.Add(portfolio);
        }

        public void Delete(int id)
        {
            Portfolio portfolio = db.Portfolios.Find(id);
            if (portfolio != null)
                db.Portfolios.Remove(portfolio);
        }

        public IEnumerable<Portfolio> Find(Func<Portfolio, bool> predicate)
        {
            return db.Portfolios.Where(predicate).ToList();
        }

        public Portfolio Get(int id)
        {
            return db.Portfolios.Find(id);
        }

        public IEnumerable<Portfolio> GetAll()
        {
            return db.Portfolios;
        }

        public void Update(Portfolio portfolio)
        {
            db.Entry(portfolio).State = EntityState.Modified;
        }
    }
}
