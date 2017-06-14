using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        bool UserIsInRole(string role, string id);
        Task CreateAsync(UserDTO userDto, int? customerId = 0);
        Task<ClaimsIdentity> AuthenticateAsync(UserDTO userDto);
        Task ChangeUserData(UserDTO userDto, int? customerId);
        void AddProfileToCustomer(Profile profile, int? customerId);
        void DeleteUser(string userId);
    }
}
