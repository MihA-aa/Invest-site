using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DALEF.EF;
using DAL.Interfaces;

namespace DALEF.Repositories
{
    class ProfileRepository : GenericRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(ApplicationContext context) : base(context)
        {
        }
        public void Delete(string id)
        {
            Profile item = dbSet.Find(id);
            if (item != null)
                dbSet.Remove(item);
        }
        public Profile Get(string id)
        {
            return dbSet.Find(id);
        }

        public bool ProfileAccess(string userid, int portfolioId)
        {
            bool check = false;
            var profile = dbSet.AsNoTracking().FirstOrDefault(p => p.Id == userid);
            var portfolios = profile?.Customer?.Portfolios;
            if (portfolios != null && portfolios.Any(p => p.Id == portfolioId))
                check = true;
            return check;
        }

        public bool IsExist(string id)
        {
            return dbSet
                .AsNoTracking()
                .Any(p => p.Id == id);
        }
    }
}
