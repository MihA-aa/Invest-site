using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using DALEF.EF;

namespace DALEF.Repositories
{
    class ColumnFormatRepository : GenericRepository<ColumnFormat>, IColumnFormatRepository
    {
        public ColumnFormatRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
