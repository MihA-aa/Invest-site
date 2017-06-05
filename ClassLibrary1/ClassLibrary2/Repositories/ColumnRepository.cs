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
    public class ColumnRepository : GenericRepository<Column>, IColumnRepository
    {
        public ColumnRepository(ApplicationContext context) : base(context)
        {
        }
        public Format GetFormatsByColumnName(string column)
        {
            return dbSet.FirstOrDefault(c => c.Name == column)?.Format;
        }
    }
}
