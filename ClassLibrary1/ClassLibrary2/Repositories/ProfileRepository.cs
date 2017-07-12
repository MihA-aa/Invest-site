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
    class ProfileRepository : GenericRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(ISession session) : base(session)
        {
        }
        public void Delete(string id)
        {
            Profile item = Session.Get<Profile>(id);
            if (item != null)
                Session.Delete(Session.Load<Profile>(id));
        }
        public Profile Get(string id)
        {
            var profile = Session.Query<Profile>().FirstOrDefault(p => p.Id == id);// Session.Get<Profile>(id);
            return new Profile {Id = "2b640904-e2f1-4822-83f1-9f5e48246051", Login = "Admin", CustomerId = 1};
            //return Session.Get<Profile>(id);
        }

        public bool ProfileAccess(string userid, int portfolioId)
        {
            bool check = false;
            var profile = Session.Query<Profile>().FirstOrDefault(p => p.Id == userid);
            var portfolios = profile?.Customer?.Portfolios;
            if (portfolios != null && portfolios.Any(p => p.Id == portfolioId))
                check = true;
            return check;
        }

        public bool IsExist(string id)
        {
            return Session.Query<Profile>()
                .Any(p => p.Id == id);
        }
    }
}
