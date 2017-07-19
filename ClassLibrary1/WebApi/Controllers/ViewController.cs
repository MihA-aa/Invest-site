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
using WebApi.Util;

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
            var views = viewService.GetViewsForUser(RequestContext.Principal.Identity.GetUserId());
            return Ok(Mapper.Map<IEnumerable<ViewDTO>, List<ViewModel>>(views));
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            ViewModel view;
            try
            {
                if (viewService.CheckAccess(RequestContext.Principal.Identity.GetUserId(), id))
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
                ModelState.AddModelError("", ex.Message);
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok(view);
        }

        [HttpPost, Transaction]
        public IHttpActionResult Post([FromBody]ViewModel view)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                viewService.CreateView(Mapper.Map<ViewModel, ViewDTO>(view), RequestContext.Principal.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok();
        }

        [HttpPut, Transaction]
        public IHttpActionResult Update([FromBody]ViewModel view)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (viewService.CheckAccess(RequestContext.Principal.Identity.GetUserId(), view.Id))
                {
                    viewService.UpdateView(Mapper.Map<ViewModel, ViewDTO>(view));
                }
                else
                {
                    throw new Exception("You don't have access to view with id: " + view.Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok();
        }

        [HttpDelete, Transaction]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (viewService.CheckAccess(RequestContext.Principal.Identity.GetUserId(), id))
                {
                    viewService.DeleteView(id);
                }
                else
                {
                    throw new Exception("You don't have access to view with id: " + id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok();
        }
    }
}
