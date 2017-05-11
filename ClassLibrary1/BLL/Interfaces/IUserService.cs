using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Infrastructure;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        //Task<OperationDetails> Create(UserDTO userDto);
        Task<ValidationException> CreateAsync(UserDTO userDto);
        Task<ClaimsIdentity> AuthenticateAsync(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        CustomerDTO GetProfile(string userId);
    }
}
