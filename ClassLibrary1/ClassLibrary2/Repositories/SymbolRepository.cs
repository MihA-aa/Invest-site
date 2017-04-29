using ClassLibrary1.Entities;
using ClassLibrary1.Interfaces;
using ClassLibrary2.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ClassLibrary2.Repositories
{
    class SymbolRepository : GenericRepository<Symbol>
    {
        public SymbolRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
