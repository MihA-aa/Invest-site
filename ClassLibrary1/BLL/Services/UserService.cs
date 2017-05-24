using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Helpers;
using BLL.Interfaces;
using DAL.Entities;
using Entity = DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using BLL.Infrastructure;

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
            User user = await Database.UserManager.FindByNameAsync(userDto.Login);
            if (user == null)
            {
                user = new User { Email = userDto.Login, UserName = userDto.Login };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Any())
                    throw new ValidationException(result.Errors.FirstOrDefault(), "");
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                var clientProfile = new Entity.Profile { Id = user.Id, Login = userDto.Login };
                Database.Profiles.Create(clientProfile);
                await Database.SaveAsync();
            }
            else
            {
                throw new ValidationException(Resource.Resource.UserAlreadyExists, "Login");
            }
        }

        public async Task<ClaimsIdentity> AuthenticateAsync(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            User user = await Database.UserManager.FindAsync(userDto.Login, userDto.Password);
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }
        
        public ProfileDTO GetProfile(string userId)
        {
            if (userId == null)
                throw new ValidationException(Resource.Resource.UserIdNotSet, "");
            var profile = Database.Profiles.Get(userId);
            if (profile == null)
                throw new ValidationException(Resource.Resource.UserNotFound, "");
            Mapper.Initialize(cfg => cfg.CreateMap<Entity.Profile, ProfileDTO>());
            return Mapper.Map<Entity.Profile, ProfileDTO>(profile);
        }

        public void UpdateProfile(ProfileDTO profile)
        {
            //ProfileValidate
            var user = Database.Profiles.Get(profile.Id);
            user.Login = profile.Login;
            Database.Profiles.Update(user);
        }
    }
}
