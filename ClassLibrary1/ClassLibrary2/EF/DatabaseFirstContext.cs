using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Views;

namespace DALEF.EF
{
    public class DatabaseFirstContext : DbContext
{
    public DatabaseFirstContext(string connectionString)
        : base(connectionString)
    {
        Database.SetInitializer<DatabaseFirstContext>(null);
    }

        public virtual DbSet<SymbolView> SymbolViews { get; set; }
        public virtual DbSet<TradeSybolView> SybolInformations{ get; set; }
        public virtual DbSet<SymbolDividend> SymbolDividends { get; set; }
        
    }
}
