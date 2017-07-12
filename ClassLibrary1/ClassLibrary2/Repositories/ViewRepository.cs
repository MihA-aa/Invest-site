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
    public class ViewRepository : GenericRepository<View>, IViewRepository
    {
        public ViewRepository(ISession session) : base(session)
        {
        }

        public bool IsExist(int id)
        {
            return Session.Query<View>()
                .Any(p => p.Id == id);
        }
    }
}
