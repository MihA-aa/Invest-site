using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IProfileService
    {
        IEnumerable<ProfileDTO> GetProfiles();
        ProfileDTO GetProfile(string id);
        Task CreateOrUpdateProfile(ProfileDTO profile);
        void DeleteProfile(string id);
    }
}
