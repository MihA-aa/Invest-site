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
    public class ViewRepository : GenericRepository<View>, IViewRepository
    {
        public ViewRepository(ApplicationContext context) : base(context)
        {
        }

        public bool IsExist(int id)
        {
            return dbSet
                .AsNoTracking()
                .Any(p => p.Id == id);
        }
    }
}
