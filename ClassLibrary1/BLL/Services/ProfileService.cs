using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Resource;
using Entity = DAL.Entities;
using DAL.Interfaces;
using BLL.Helpers;
using BLL.Interfaces;

namespace BLL.Services
{
    public class ProfileService: BaseService,IProfileService
    {
        IUserService userService { get; }

        public ProfileService(IUnitOfWork uow, IMapper map, IValidateService vd, IUserService us):base(uow, vd, map)
        {
            userService = us;
        }

        public IEnumerable<ProfileDTO> GetProfiles()
        {
            return IMapper.Map<IEnumerable<Entity.Profile>, List<ProfileDTO>>(db.Profiles.GetAll());
        }

        public ProfileDTO GetProfile(string id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.ProfileIdNotSet, "");
            var profile = db.Profiles.Get(id);
            if (profile == null)
                throw new ValidationException(Resource.Resource.ProfileNotFound, "");
            return IMapper.Map<Entity.Profile, ProfileDTO>(profile);
        }

        public async Task CreateOrUpdateProfile(ProfileDTO profile)
        {
            if (profile == null)
                throw new ValidationException(Resource.Resource.ProfileNullReference, "");
            if (db.Profiles.IsExist(profile.Id))
                await userService.ChangeUserData(new UserDTO { Id = profile.Id, Login = profile.Login}, profile.CustomerId);
            else
                await userService.CreateAsync(new UserDTO { Id = profile.Id,
                    Login = profile.Login, Password = "Kamoqw1_wer21", Role = "user" }, profile.CustomerId);
        }

        public void DeleteProfile(string id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.ProfileIdNotSet, "");
            if (!db.Profiles.IsExist(id))
                throw new ValidationException(Resource.Resource.ProfileNotFound, "");
            db.Profiles.Delete(id);
            userService.DeleteUser(id);
        }
    }
}
