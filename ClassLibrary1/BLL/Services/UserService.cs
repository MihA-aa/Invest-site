using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Resource;
using BLL.Helpers;
using BLL.Interfaces;
using DAL.Entities;
using Entity = DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using BLL.Infrastructure;

namespace BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork uow, IValidateService vd, IMapper map): base(uow, vd, map){}

        public async Task CreateAsync(UserDTO userDto, int? customerId = 0)
        {
            db.BeginTransaction();
            validateService.Validate(userDto);
            UserEntity user = await db.UserManager.FindByNameAsync(userDto.Login);
            if (user == null)
            {
                user = new UserEntity { Email = userDto.Login, UserName = userDto.Login };
                var result = await db.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Any())
                    throw new ValidationException(result.Errors.FirstOrDefault(), "");
                await db.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                var clientProfile = new Entity.Profile { Id = user.Id, Login = userDto.Login };
                if (customerId != 0)
                {
                    AddProfileToCustomer(clientProfile, customerId);
                    await db.UserManager.AddToRoleAsync(user.Id, "Employee");
                }
                db.Profiles.Create(clientProfile);
                //await db.SaveAsync();
            }
            else
            {
                throw new ValidationException(Resource.Resource.UserAlreadyExists, "Login");
            }
            db.Commit();
        }

        public async Task ChangeUserData(UserDTO userDto, int? customerId)
        {
            if (db.UserManager.Users.FirstOrDefault(x => x.UserName == userDto.Login)== null
                || db.UserManager.Users.FirstOrDefault(x => x.UserName == userDto.Login)?.Id == userDto.Id)
            {
                var user = db.UserManager.FindById(userDto.Id);
                validateService.ValidateOnlyLogin(userDto);
                user.UserName = userDto.Login;
                var updateResult = await db.UserManager.UpdateAsync(user);
                if (updateResult.Errors.Any())
                    throw new ValidationException(updateResult.Errors.FirstOrDefault(), "");
                var profile = db.Profiles.Get(userDto.Id);
                profile.Login = userDto.Login;
                AddProfileToCustomer(profile, customerId);
                await db.UserManager.AddToRoleAsync(user.Id, "Employee");
                //await db.SaveAsync();
            }
            else
            {
                throw new ValidationException(Resource.Resource.UserAlreadyExists, "Login");
            }
        }

        public async Task<ClaimsIdentity> AuthenticateAsync(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            UserEntity user = await db.UserManager.FindAsync(userDto.Login, userDto.Password);
            if (user != null)
                claim = await db.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public void DeleteUser(string userId)
        {
            var user = db.UserManager.FindById(userId);
            if(user!=null)
                db.UserManager.Delete(user);
        }

        public void AddProfileToCustomer(Entity.Profile profile, int? customerId)
        {
            if (profile == null)
                throw new ValidationException(Resource.Resource.ProfileNullReference, "");
            if (customerId == null)
                throw new ValidationException(Resource.Resource.CustomerIdNotSet, "");
            if (!db.Customers.IsExist(customerId.Value))
                throw new ValidationException(Resource.Resource.ProfileNotFound, "");
            db.Customers.AddProfileToCustomer(profile, customerId.Value);
        }

        public bool UserIsInRole(string role, string id)
        {
            if (db.UserManager.FindById(id) == null)
                return false;
            return db.UserManager.IsInRole(id, role);
        }
    }
}
