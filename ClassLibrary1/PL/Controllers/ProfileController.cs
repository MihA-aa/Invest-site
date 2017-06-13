using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using PL.Models;
using BLL.Helpers;

namespace PL.Controllers
{
    public class ProfileController : Controller
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ProfileController));
        private IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpGet]
        public ActionResult Save(string id)
        {
            ProfileModel profile = null;
            try
            {
                if (id == "0")
                    return PartialView();
                profile = Mapper.Map<ProfileDTO, ProfileModel>(profileService.GetProfile(id));
            }
            catch (ValidationException ex)
            {
                logger.Error(ex.ToString());
            }
            return PartialView(profile);
        }

        [HttpPost]
        public ActionResult Save(ProfileModel profile)
        {
            bool status = true;
            string message = "", property = "";
            if (ModelState.IsValid)
            {
                try
                {
                    profileService.CreateOrUpdateProfile(Mapper.Map<ProfileModel, ProfileDTO>(profile));
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
        public ActionResult Delete(string id)
        {
            try
            {
                ViewBag.Id = profileService.GetProfile(id).Id;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return HttpNotFound();
            }
            return PartialView();
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteProfile(string id)
        {
            bool status = true;
            try
            {
                profileService.DeleteProfile(id);
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