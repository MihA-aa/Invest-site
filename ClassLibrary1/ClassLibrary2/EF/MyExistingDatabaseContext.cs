using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Views;

namespace DALEF.EF
{
    public class MyExistingDatabaseContext : DbContext
{
    public MyExistingDatabaseContext(string connectionString)
        : base(connectionString)
    {
        Database.SetInitializer<MyExistingDatabaseContext>(null);
    }

        public virtual DbSet<SymbolView> SymbolViews { get; set; }
    }
}
