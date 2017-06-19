using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ViewController : BaseController
    {
        private IViewService viewService;

        public ViewController(IViewService viewService)
        {
            this.viewService = viewService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var views = viewService.GetViewsForUser("1aaa023d-e950-47fc-9c3f-54fbffcc99cf"
                /*RequestContext.Principal.Identity.GetUserId()*/);
            return Ok(Mapper.Map<IEnumerable<ViewDTO>, List<ViewModel>>(views));
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            ViewModel view = new ViewModel();
            try
            {
                view = Mapper.Map<ViewDTO, ViewModel>(viewService.GetView(id));
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok(view);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]ViewModel view)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                viewService.CreateView(Mapper.Map<ViewModel, ViewDTO>(view), "1aaa023d-e950-47fc-9c3f-54fbffcc99cf");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update([FromBody]ViewModel view)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                viewService.UpdateView(Mapper.Map<ViewModel, ViewDTO>(view));
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
                viewService.DeleteView(id);
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
