using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using Entity = DAL.Entities;
using DAL.Interfaces;
using BLL.Helpers;
using BLL.Interfaces;

namespace BLL.Services
{
    public class ProfileService: IProfileService
    {
        IUnitOfWork db { get; }
        IMapper IMapper { get; }
        IUserService userService { get; }

        public ProfileService(IUnitOfWork uow, IMapper map, IUserService userService)
        {
            db = uow;
            IMapper = map;
            this.userService = userService;
        }

        public IEnumerable<ProfileDTO> GetProfiles()
        {
            return IMapper.Map<IEnumerable<Entity.Profile>, List<ProfileDTO>>(db.Profiles.GetAll());
        }

        public ProfileDTO GetProfile(string id)
        {
            if (id == null)
                throw new ValidationException("Profile Id Not Set", "");
            var profile = db.Profiles.Get(id);
            if (profile == null)
                throw new ValidationException("Profile Not Found", "");
            return IMapper.Map<Entity.Profile, ProfileDTO>(profile);
        }

        public async Task CreateOrUpdateProfile(ProfileDTO profile)
        {
            if (profile == null)
                throw new ValidationException("Profile Null Reference", "");
            if (db.Profiles.IsExist(profile.Id))
                await userService.ChangeUserData(new UserDTO { Id = profile.Id, Login = profile.Login}, profile.CustomerId);
            else
                await userService.CreateAsync(new UserDTO { Id = profile.Id,
                    Login = profile.Login, Password = "Kamoqw1_wer21", Role = "user" }, profile.CustomerId);
        }

        public void DeleteProfile(string id)
        {
            if (id == null)
                throw new ValidationException("Profile Id Not Set", "");
            if (!db.Profiles.IsExist(id))
                throw new ValidationException("Profile Not Found", "");
            db.Profiles.Delete(id);
            db.Save();
        }
    }
}
