using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DALEF.EF;

namespace DALEF.Repositories
{
    class DividendRepository : GenericRepository<Dividend>
    {
        public DividendRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
