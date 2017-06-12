using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IProfileRepository : IRepository<Profile>
    {
        void Delete(string id);
        Profile Get(string id);
        bool IsExist(string id);
    }
}
