using ClassLibrary1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary2.EF;

namespace ClassLibrary2.Repositories
{
    public class PortfolioRepository : GenericRepository<Portfolio>
    {
        public PortfolioRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
