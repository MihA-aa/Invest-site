using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using System.Linq.Dynamic;
using BLL.Interfaces;
using PL.Models;

namespace PL.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AdminController : BaseController
    {
        private ICustomerService customerService;

        public AdminController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoadCustomerData()
        {
            var draw = Request.Form?.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int totalRecords = 0;

            var customersDto = customerService.GetCustomers();

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                customersDto = customersDto.OrderBy(sortColumn + " " + sortColumnDir);
            }

            totalRecords = customersDto.Count();
            customersDto = customersDto.Skip(skip).Take(pageSize).ToList();
            var data = Mapper.Map<IEnumerable<CustomerDTO>, List<CustomerModel>>(customersDto);
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = data }, JsonRequestBehavior.AllowGet);
        }
    }
}