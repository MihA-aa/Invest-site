using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface ICustomerService
    {
        Customer GetCustomerByProfileId(string id);
        IEnumerable<CustomerDTO> GetCustomers();
        CustomerDTO GetCustomer(int? id);
        void CreateOrUpdateCustomer(CustomerDTO customer);
        void UpdateCustomer(CustomerDTO customerDto);
        void DeleteCustomer(int? id);
    }
}
