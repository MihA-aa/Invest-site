using ClassLibrary1.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.EF
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(string conectionString) : base(conectionString) { }
        static ApplicationContext()
        {
            Database.SetInitializer<ApplicationContext>(new StoreDbInitializer());
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Symbol> Symbols { get; set; }
        public DbSet<User> Users { get; set; }

    }

    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        protected override void Seed(ApplicationContext db)
        {
            db.Symbols.Add(new Symbol { Id = 1, Name = "AAT", SymbolType = Symbols.Option });
            db.SaveChanges();
        }
    }
}
