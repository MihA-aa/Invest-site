using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using WebApi.Models;
using WebApi.Util;

namespace WebApi.Controllers
{
    public class ViewTemplateController : BaseController
    {
        private IViewTemplateService viewTemplateService;

        public ViewTemplateController(IViewTemplateService viewTemplateService)
        {
            this.viewTemplateService = viewTemplateService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var viewTemplates = viewTemplateService.GetViewTemplatesForUser(RequestContext.Principal.Identity.GetUserId());
            return Ok(Mapper.Map<IEnumerable<ViewTemplateDTO>, List<ViewTemplateModel>>(viewTemplates));
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            ViewTemplateModel viewTemplate;
            try
            {
                if (viewTemplateService.CheckAccess(RequestContext.Principal.Identity.GetUserId(), id))
                {
                    viewTemplate = Mapper.Map<ViewTemplateDTO, ViewTemplateModel>(viewTemplateService.GetViewTemplate(id));
                }
                else
                {
                    throw new Exception("You don't have access to ViewTemplate with id: " + id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok(viewTemplate);
        }

        [HttpPost, Transaction]
        public IHttpActionResult Post([FromBody]ViewTemplateModel viewTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                viewTemplateService.CreateViewTemplate(Mapper.Map<ViewTemplateModel, ViewTemplateDTO>(viewTemplate), RequestContext.Principal.Identity.GetUserId());
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
        public IHttpActionResult Update([FromBody]ViewTemplateModel viewTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (viewTemplateService.CheckAccess(RequestContext.Principal.Identity.GetUserId(), viewTemplate.Id))
                {
                    viewTemplateService.UpdateViewTemplate(Mapper.Map<ViewTemplateModel, ViewTemplateDTO>(viewTemplate));
                }
                else
                {
                    throw new Exception("You don't have access to ViewTemplate with id: " + viewTemplate.Id);
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
                if (viewTemplateService.CheckAccess(RequestContext.Principal.Identity.GetUserId(), id))
                {
                    viewTemplateService.DeleteViewTemplate(id);
                }
                else
                {
                    throw new Exception("You don't have access to ViewTemplate with id: " + id);
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
