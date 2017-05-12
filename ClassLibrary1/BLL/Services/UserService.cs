using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using Entity = DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using BLL.Infrastructure;
using Resources;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }
        IValidateService validateService { get; }

        public UserService(IUnitOfWork uow, IValidateService vd)
        {
            Database = uow;
            validateService = vd;
        }

        public async Task CreateAsync(UserDTO userDto)
        {
            validateService.Validate(userDto);
            User user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new User { Email = userDto.Email, UserName = userDto.Name };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    throw new ValidationException(result.Errors.FirstOrDefault(), "");
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                var clientProfile = new Entity.Profile { Id = user.Id, Name = userDto.Name };
                Database.Profiles.Create(clientProfile);
                await Database.SaveAsync();
            }
            else
            {
                throw new ValidationException(Resource.UserAlreadyExists, Resource.Email);
            }
        }

        public async Task<ClaimsIdentity> AuthenticateAsync(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            User user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }
        
        public ProfileDTO GetProfile(string userId)
        {
            var profile = Database.Profiles.Get(userId);
            Mapper.Initialize(cfg => cfg.CreateMap<Entity.Profile, ProfileDTO>());
            return Mapper.Map<Entity.Profile, ProfileDTO>(profile);
        }

        public void UpdateProfile(ProfileDTO profile)
        {
            //ProfileValidate
            var user = Database.Profiles.Get(profile.Id);
            user.Name = profile.Name;
            Database.Profiles.Update(user);
        }
    }
}
