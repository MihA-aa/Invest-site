﻿using System;
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
                if (customerId != 0)
                {
                    AddProfileToCustomer(clientProfile, customerId);
                    await Database.UserManager.AddToRoleAsync(user.Id, "Employee");
                }
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
            if (Database.UserManager.Users.FirstOrDefault(x => x.UserName == userDto.Login)== null
                || Database.UserManager.Users.FirstOrDefault(x => x.UserName == userDto.Login)?.Id == userDto.Id)
            {
                var user = Database.UserManager.FindById(userDto.Id);
                validateService.ValidateOnlyLogin(userDto);
                user.UserName = userDto.Login;
                var updateResult = await Database.UserManager.UpdateAsync(user);
                if (updateResult.Errors.Any())
                    throw new ValidationException(updateResult.Errors.FirstOrDefault(), "");
                var profile = Database.Profiles.Get(userDto.Id);
                profile.Login = userDto.Login;
                AddProfileToCustomer(profile, customerId);
                await Database.UserManager.AddToRoleAsync(user.Id, "Employee");
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

        public void DeleteUser(string userId)
        {
            var user = Database.UserManager.FindById(userId);
            if(user!=null)
                Database.UserManager.Delete(user);
        }

        public void AddProfileToCustomer(Entity.Profile profile, int? customerId)
        {
            if (profile == null)
                throw new ValidationException(Resource.Resource.ProfileNullReference, "");
            if (customerId == null)
                throw new ValidationException(Resource.Resource.CustomerIdNotSet, "");
            if (!Database.Customers.IsExist(customerId.Value))
                throw new ValidationException(Resource.Resource.ProfileNotFound, "");
            Database.Customers.AddProfileToCustomer(profile, customerId.Value);
        }

        public bool UserIsInRole(string role, string id)
        {
            if (Database.UserManager.FindById(id) == null)
                return false;
            return Database.UserManager.IsInRole(id, role);
        }
    }
}
