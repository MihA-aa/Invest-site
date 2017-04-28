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
    class SymbolRepository : IRepository<Symbol>
    {
        ApplicationContext db;
        public SymbolRepository(ApplicationContext context)
        {
            this.db = context;
        }
        public void Create(Symbol symbol)
        {
            db.Symbols.Add(symbol);
        }

        public void Delete(int id)
        {
            Symbol symbol = db.Symbols.Find(id);
            if (symbol != null)
                db.Symbols.Remove(symbol);
        }

        public IEnumerable<Symbol> Find(Func<Symbol, bool> predicate)
        {
            return db.Symbols.Where(predicate).ToList();
        }

        public Symbol Get(int id)
        {
            return db.Symbols.Find(id);
        }

        public IEnumerable<Symbol> GetAll()
        {
            return db.Symbols;
        }

        public void Update(Symbol symbol)
        {
            db.Entry(symbol).State = EntityState.Modified;
        }
    }
}
