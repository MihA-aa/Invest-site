using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class CustomerService: ICustomerService
    {
        IUnitOfWork db { get; }

        public CustomerService(IUnitOfWork uow)
        {
            db = uow;
        }

        public Customer GetCustomerByProfileId(string id)
        {
            return db.Customers.GetCustomerByProfileId(id);
        }
    }
}
