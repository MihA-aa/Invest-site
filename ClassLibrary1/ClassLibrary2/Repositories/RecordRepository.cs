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
    public class RecordRepository: GenericRepository<Record>, IRecordRepository
    {
        public RecordRepository(ISession session) : base(session) {}

        public bool IsExist(int id)
        {
            return Session
                .Query<Record>()
                .Any(p => p.Id == id);
        }
    }
}
