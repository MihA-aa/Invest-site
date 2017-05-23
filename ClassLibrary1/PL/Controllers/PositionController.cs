using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using PL.Models;
using BLL.Infrastructure;

namespace PL.Controllers
{
    public class PositionController : Controller
    {
        private IPositionService positionService;

        public PositionController(IPositionService positionService)
        {
            this.positionService = positionService;
        }
        [HttpGet]
        public ActionResult Save(int? id)
        {
            if(id == 0)
                return PartialView();
            var positionDto = positionService.GetPosition(id);
            Mapper.Initialize(cfg => cfg.CreateMap<PositionDTO, PositionModel>());
            var position = Mapper.Map<PositionDTO, PositionModel>(positionDto);
            return PartialView(position);
        }

        [HttpPost]
        public ActionResult Save(PositionModel position)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    status = true;
                    Mapper.Initialize(cfg => cfg.CreateMap<PositionModel, PositionDTO>());
                    var positionDto = Mapper.Map<PositionModel, PositionDTO>(position);
                    positionService.CreatePosition(positionDto);
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                    status = false;
                }
                
            }
            return new JsonResult { Data = new { status = status } };
        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                 ViewBag.Id = positionService.GetPosition(id).Id;
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
            return PartialView();
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePosition(int? id)
        {
            bool status = true;
            try
            {
               positionService.DeletePosition(id);
            }
            catch (Exception)
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}