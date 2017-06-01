using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Helpers;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ViewTemplateService: IViewTemplateService
    {
        IUnitOfWork db { get; }

        public ViewTemplateService(IUnitOfWork uow)
        {
            db = uow;
        }

        public IEnumerable<ViewTemplateDTO> GetViewTemplates()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplate, ViewTemplateDTO>());
            return Mapper.Map<IEnumerable<ViewTemplate>, List<ViewTemplateDTO>>(db.ViewTemplates.GetAll());
        }
        
        public ViewTemplateDTO GetViewTemplate(int? id)
        {
            if (id == null)
                throw new ValidationException("Resource.Resource.ViewTemplateIdNotSet", "");
            var viewTemplate = db.ViewTemplates.Get(id.Value);
            if (viewTemplate == null)
                throw new ValidationException("Resource.Resource.ViewTemplateNotFound", "");
            Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplate, ViewTemplateDTO>());
            return Mapper.Map<ViewTemplate, ViewTemplateDTO>(viewTemplate);
        }
    }
}
