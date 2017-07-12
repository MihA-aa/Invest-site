using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using NHibernate;

namespace DALEF.Repositories
{
    public class FormatRepository: GenericRepository<Format>, IFormatRepository
    {
        public FormatRepository(ISession session) : base(session) {}
    }
}
