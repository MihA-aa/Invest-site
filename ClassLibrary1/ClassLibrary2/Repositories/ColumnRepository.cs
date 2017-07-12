using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using NHibernate;
using NHibernate.Linq;

namespace DALEF.Repositories
{
    public class ColumnRepository : GenericRepository<Column>, IColumnRepository
    {
        public ColumnRepository(ISession session) : base(session)
        {
        }

        public Format GetFormatsByColumnName(string column)
        {
            return Session.Query<Column>()
                .FirstOrDefault(c => c.Name == column)?.Format;
        }

        public Column GetColumnByColumnName(string column)
        {
            return Session.Query<Column>()
                .FirstOrDefault(c => c.Name == column);
        }
    }
}
