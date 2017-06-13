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
        private IProfileService profileService;

        public AdminController(ICustomerService customerService, IProfileService profileService)
        {
            this.customerService = customerService;
            this.profileService = profileService;
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

        [HttpPost]
        public ActionResult LoadProfileData()
        {
            var draw = Request.Form?.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int totalRecords = 0;

            var profilesDto = profileService.GetProfiles();

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                profilesDto = profilesDto.OrderBy(sortColumn + " " + sortColumnDir);
            }

            totalRecords = profilesDto.Count();
            profilesDto = profilesDto.Skip(skip).Take(pageSize).ToList();
            var data = Mapper.Map<IEnumerable<ProfileDTO>, List<ProfileModel>>(profilesDto);
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = data }, JsonRequestBehavior.AllowGet);
        }
    }
}