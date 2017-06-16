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
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PositionController));
        private IPositionService PositionService;

        public PositionController(IPositionService PositionService)
        {
            this.PositionService = PositionService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var positions = PositionService.GetPositions();
            return Ok(Mapper.Map<IEnumerable<PositionDTO>, List<PositionModel>>(positions));
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            PositionModel position = new PositionModel();
            try
            {
                position = Mapper.Map<PositionDTO, PositionModel>(PositionService.GetPosition(id));
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
                PositionService.UpdatePosition(Mapper.Map<PositionModel, PositionDTO>(position));
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
                PositionService.DeletePosition(id);
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
