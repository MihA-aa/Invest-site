using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class PositionController : BaseController
    {
        private IPositionService PositionService;

        public PositionController(IPositionService PositionService)
        {
            this.PositionService = PositionService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var positions = PositionService.GetPositionsForUser("1aaa023d-e950-47fc-9c3f-54fbffcc99cf");
            return Ok(Mapper.Map<IEnumerable<PositionDTO>, List<PositionModel>>(positions));
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            PositionModel position;
            try
            {
                if (PositionService.CheckAccess("1aaa023d-e950-47fc-9c3f-54fbffcc99cf", id))
                {
                    position = Mapper.Map<PositionDTO, PositionModel>(PositionService.GetPosition(id));
                }
                else
                {
                    throw new Exception("You don't have access to position with id: " + id);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok(position);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]PositionModel position, int portfolioId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PositionService.CreatePosition(Mapper.Map<PositionModel, PositionDTO>(position), portfolioId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update([FromBody]PositionModel position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (PositionService.CheckAccess("1aaa023d-e950-47fc-9c3f-54fbffcc99cf", position.Id))
                {
                    PositionService.UpdatePosition(Mapper.Map<PositionModel, PositionDTO>(position));
                }
                else
                {
                    throw new Exception("You don't have access to position with id: " + position.Id);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (PositionService.CheckAccess("1aaa023d-e950-47fc-9c3f-54fbffcc99cf", id))
                {
                    PositionService.DeletePosition(id);
                }
                else
                {
                    throw new Exception("You don't have access to position with id: " + id);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok();
        }
    }
}
