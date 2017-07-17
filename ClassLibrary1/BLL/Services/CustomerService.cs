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
    public class CustomerService: BaseService, ICustomerService
    {
        public CustomerService(IUnitOfWork uow, IValidateService vd, IMapper map) : base(uow, vd, map){}
        
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
                throw new ValidationException(Resource.Resource.CustomerIdNotSet, "");
            var customer = db.Customers.Get(id.Value);
            if (customer == null)
                throw new ValidationException(Resource.Resource.CustomerNotFound, "");
            return IMapper.Map<Customer, CustomerDTO>(customer);
        }

        public void CreateOrUpdateCustomer(CustomerDTO customer)
        {
            if (customer == null)
                throw new ValidationException(Resource.Resource.CustomerNullReference, "");
            if (db.Customers.IsExist(customer.Id))
                UpdateCustomer(customer);
            else
                CreateCustomer(customer);
        }

        public void CreateCustomer(CustomerDTO customerDto)
        {
            db.BeginTransaction();
            try
            {
                if (customerDto == null)
                    throw new ValidationException(Resource.Resource.CustomerNullReference, "");
                db.Customers.Create(IMapper.Map<CustomerDTO, Customer>(customerDto));

                db.Commit();
            }
            catch (Exception ex)
            {
                db.RollBack();
                throw ex;
            }
        }

        public void UpdateCustomer(CustomerDTO customerDto)
        {
            db.BeginTransaction();
            try
            {
                if (customerDto == null)
                    throw new ValidationException(Resource.Resource.CustomerNullReference, "");
                if (!db.Customers.IsExist(customerDto.Id))
                    throw new ValidationException(Resource.Resource.CustomerNotFound, "");

                var customerFromDb = db.Customers.Get(customerDto.Id);
                customerFromDb.Name = customerDto.Name;
                db.Customers.Update(customerFromDb);

                db.Commit();
            }
            catch (Exception ex)
            {
                db.RollBack();
                throw ex;
            }
        }

        public void DeleteCustomer(int? id)
        {
            db.BeginTransaction();
            try
            {
                if (id == null)
                    throw new ValidationException(Resource.Resource.CustomerIdNotSet, "");
                if (!db.Customers.IsExist(id.Value))
                    throw new ValidationException(Resource.Resource.CustomerNotFound, "");
                db.Customers.Delete(id.Value);

                db.Commit();
            }
            catch (Exception ex)
            {
                db.RollBack();
                throw ex;
            }
        }
    }
}
