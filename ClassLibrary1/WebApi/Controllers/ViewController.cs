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
            ViewModel view;
            try
            {
                if (viewService.CheckAccess("1aaa023d-e950-47fc-9c3f-54fbffcc99cf", id))
                {
                    view = Mapper.Map<ViewDTO, ViewModel>(viewService.GetView(id));
                }
                else
                {
                    throw new Exception("You don't have access to view with id: " + id);
                }
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
                if (viewService.CheckAccess("1aaa023d-e950-47fc-9c3f-54fbffcc99cf", view.Id))
                {
                    viewService.UpdateView(Mapper.Map<ViewModel, ViewDTO>(view), RequestContext.Principal.Identity.GetUserId());
                }
                else
                {
                    throw new Exception("You don't have access to view with id: " + view.Id);
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
                if (viewService.CheckAccess("1aaa023d-e950-47fc-9c3f-54fbffcc99cf", id))
                {
                    viewService.DeleteView(id, RequestContext.Principal.Identity.GetUserId());
                }
                else
                {
                    throw new Exception("You don't have access to view with id: " + id);
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
