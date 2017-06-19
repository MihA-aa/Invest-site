using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.DTO;
using BLL.Interfaces;
using WebApi.Models;

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
            var viewTemplates = viewTemplateService.GetViewTemplatesForUser("1aaa023d-e950-47fc-9c3f-54fbffcc99cf"
                /*RequestContext.Principal.Identity.GetUserId()*/);
            return Ok(Mapper.Map<IEnumerable<ViewTemplateDTO>, List<ViewTemplateModel>>(viewTemplates));
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            ViewTemplateModel viewTemplate;
            try
            {
                if (viewTemplateService.CheckAccess("1aaa023d-e950-47fc-9c3f-54fbffcc99cf", id))
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
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok(viewTemplate);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]ViewTemplateModel viewTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                viewTemplateService.CreateViewTemplate(Mapper.Map<ViewTemplateModel, ViewTemplateDTO>(viewTemplate), "1aaa023d-e950-47fc-9c3f-54fbffcc99cf");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update([FromBody]ViewTemplateModel viewTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (viewTemplateService.CheckAccess("1aaa023d-e950-47fc-9c3f-54fbffcc99cf", viewTemplate.Id))
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
                if (viewTemplateService.CheckAccess("1aaa023d-e950-47fc-9c3f-54fbffcc99cf", id))
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
                logger.Error(ex.ToString());
                return BadRequest(ex.ToString());
            }
            return Ok();
        }
    }
}
