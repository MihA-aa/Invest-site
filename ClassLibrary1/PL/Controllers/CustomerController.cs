using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.DTO.Enums;
using BLL.Interfaces;
using PL.Models;
using PL.Util;
using BLL.Helpers;
using log4net;

namespace PL.Controllers
{
    public class CustomerController : BaseController
    {
        private ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        public ActionResult Save(int? id)
        {
            CustomerModel customer = null;
            try
            {
                if (id == 0)
                    return PartialView();
                customer = Mapper.Map<CustomerDTO, CustomerModel>(customerService.GetCustomer(id));
            }
            catch (ValidationException ex)
            {
                logger.Error(ex.ToString());
            }
            return PartialView(customer);
        }

        [HttpPost, Transaction]
        public ActionResult Save(CustomerModel customer)
        {
            bool status = true;
            string message = "", property = "";
            if (ModelState.IsValid)
            {
                try
                {
                    customerService.CreateOrUpdateCustomer(Mapper.Map<CustomerModel, CustomerDTO>(customer));
                }
                catch (ValidationException ex)
                {
                    logger.Error(ex.ToString());
                    status = false;
                    property = ex.Property;
                    message = ex.Message;
                }
            }
            return new JsonResult { Data = new { status = status, prop = property, message = message } };
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                ViewBag.Id = customerService.GetCustomer(id).Id;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return HttpNotFound();
            }
            return PartialView();
        }

        [HttpPost, Transaction]
        [ActionName("Delete")]
        public ActionResult DeleteCustomer(int? id)
        {
            bool status = true;
            try
            {
                customerService.DeleteCustomer(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}