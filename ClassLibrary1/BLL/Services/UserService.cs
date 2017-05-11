﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using BLL.Infrastructure;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<ValidationException> CreateAsync(UserDTO userDto)
        {
            //UserDTOValidate
            User user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new User { Email = userDto.Email, UserName = userDto.Email };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new ValidationException( result.Errors.FirstOrDefault(), "");
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                Customer clientProfile = new Customer { Id = user.Id, Name = userDto.Name };
                Database.Customers.Create(clientProfile);
                //await Database.Save();
                Database.Save();
                return new ValidationException("Регистрация успешно пройдена", "");
            }
            else
            {
                return new ValidationException("Пользователь с таким логином уже существует", "Email");
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
        
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new Role { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await CreateAsync(adminDto);
            //CreateAsync(adminDto);
        }

        public CustomerDTO GetProfile(string userId)
        {
            var profile = Database.Customers.Get(userId);
            Mapper.Initialize(cfg => cfg.CreateMap<Customer, CustomerDTO>());
            return Mapper.Map<Customer, CustomerDTO>(profile);
        }

        public void UpdateProfile(CustomerDTO customer)
        {
            //UserDTOValidate
            var user = Database.Customers.Get(customer.Id);
            user.Name = customer.Name;
            Database.Customers.Update(user);
        }
    }
}
