using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using BLL.Helpers;

namespace BLL.Services
{
    public class CustomerService: ICustomerService
    {
        IUnitOfWork db { get; }
        IMapper IMapper { get; }

        public CustomerService(IUnitOfWork uow, IMapper map)
        {
            db = uow;
            IMapper = map;
        }
        
        public Customer GetCustomerByProfileId(string id)
        {
            return db.Customers.GetCustomerByProfileId(id);
        }

        public IEnumerable<CustomerDTO> GetCustomers()
        {
            return IMapper.Map<IEnumerable<Customer>, List<CustomerDTO>>(db.Customers.GetAll());
        }

        public CustomerDTO GetCustomer(int? id)
        {
            if (id == null)
                throw new ValidationException("Customer Id Not Set", "");
            var customer = db.Customers.Get(id.Value);
            if (customer == null)
                throw new ValidationException("Customer Not Found", "");
            return IMapper.Map<Customer, CustomerDTO>(customer);
        }

        public void CreateOrUpdateCustomer(CustomerDTO customer)
        {
            if (customer == null)
                throw new ValidationException("Customer Null Reference", "");
            if (db.Customers.IsExist(customer.Id))
                UpdateCustomer(customer);
            else
                CreateCustomer(customer);
        }

        public void CreateCustomer(CustomerDTO customerDto)
        {
            if (customerDto == null)
                throw new ValidationException("CustomerDTO Null Reference", "");
            db.Customers.Create(IMapper.Map<CustomerDTO, Customer>(customerDto));
            db.Save();
        }

        public void UpdateCustomer(CustomerDTO customerDto)
        {
            if (customerDto == null)
                throw new ValidationException("CustomerDTO Null Reference", "");
            if (!db.Customers.IsExist(customerDto.Id))
                throw new ValidationException("Customer Not Found", "");
            db.Customers.Update(IMapper.Map<CustomerDTO, Customer>(customerDto));
            db.Save();
        }

        public void DeleteCustomer(int? id)
        {
            if (id == null)
                throw new ValidationException("Customer Id Not Set", "");
            if (!db.Customers.IsExist(id.Value))
                throw new ValidationException("Customer Not Found", "");
            db.Customers.Delete(id.Value);
            db.Save();
        }
    }
}
