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
        IMapper IMapper { get; }

        public UserService(IUnitOfWork uow, IValidateService vd, IMapper map)
        {
            Database = uow;
            validateService = vd;
            IMapper = map;
        }

        public async Task CreateAsync(UserDTO userDto, int? customerId = 0)
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
                if(customerId != 0)
                    AddProfileToCustomer(clientProfile, customerId);
                Database.Profiles.Create(clientProfile);
                await Database.SaveAsync();
            }
            else
            {
                throw new ValidationException(Resource.Resource.UserAlreadyExists, "Login");
            }
        }

        public async Task ChangeUserData(UserDTO userDto, int? customerId)
        {
            if (Database.UserManager.Users.FirstOrDefault(x => x.UserName == userDto.Login) == null)
            {
                var user = Database.UserManager.FindById(userDto.Id);
                userDto.Password = "Kamoqw1_wer21";
                validateService.Validate(userDto);
                user.UserName = userDto.Login;
                var updateResult = await Database.UserManager.UpdateAsync(user);
                if (updateResult.Errors.Any())
                    throw new ValidationException(updateResult.Errors.FirstOrDefault(), "");
                var profile = Database.Profiles.Get(userDto.Id);
                profile.Login = userDto.Login;
                AddProfileToCustomer(profile, customerId);
                await Database.SaveAsync();
            }
            else
            {
                throw new ValidationException("Please select a different username", "Login");
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
        public void AddProfileToCustomer(Entity.Profile profile, int? customerId)
        {
            if (profile == null)
                throw new ValidationException("Profile null Reference", "");
            if (customerId == null)
                throw new ValidationException("Customer Id Not Set", "");
            if (!Database.Customers.IsExist(customerId.Value))
                throw new ValidationException("Profile Not Found", "");
            Database.Customers.AddProfileToCustomer(profile, customerId.Value);
        }
    }
}
