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
        IValidateService validateService { get; }

        public ViewTemplateService(IUnitOfWork uow, IValidateService vd)
        {
            db = uow;
            validateService = vd;
        }

        public IEnumerable<ViewTemplateDTO> GetViewTemplates()
        {
            var viewTemplates = db.ViewTemplates.GetAll();
            if (viewTemplates == null)
            {
                return null;
            }
            Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplate, ViewTemplateDTO>()
                .ForMember("SortColumnId", opt => opt.MapFrom(src => src.SortColumn.Id)));
            return Mapper.Map<IEnumerable<ViewTemplate>, List<ViewTemplateDTO>>(viewTemplates);
        }

        public string GetNameByTemplateId(int? id)
        {
            if (id == null)
                throw new ValidationException("ViewTemplate Id Not Set", "");
            var viewTemplate = db.ViewTemplates.Get(id.Value);
            if (viewTemplate == null)
                throw new ValidationException("ViewTemplate Not Found", "");
            return viewTemplate.Name;
        }

        public ViewTemplateDTO GetViewTemplate(int? id)
        {
            if (id == null)
                throw new ValidationException("ViewTemplate Id Not Set", "");
            var viewTemplate = db.ViewTemplates.Get(id.Value);
            if (viewTemplate == null)
                throw new ValidationException("ViewTemplate Not Found", "");
            Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplate, ViewTemplateDTO>());
            return Mapper.Map<ViewTemplate, ViewTemplateDTO>(viewTemplate);
        }

        public IEnumerable<ViewTemplateColumnDTO> GetViewTemplateColumns(int? viewTemplateId)
        {
            if (viewTemplateId == null)
                throw new ValidationException("View Template id not set", "");
            var viewTemplate = db.ViewTemplates.Get(viewTemplateId.Value);
            if (viewTemplate == null)
                throw new ValidationException("View Template not found", "");
            Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplateColumn, ViewTemplateColumnDTO>());
            return Mapper.Map<IEnumerable<ViewTemplateColumn>, List<ViewTemplateColumnDTO>>(viewTemplate.Columns.ToList());
        }

        public void CreateOrUpdateViewTemplate(ViewTemplateDTO viewTemplate)
        {
            if (viewTemplate == null)
                throw new ValidationException("ViewTemplateDTO Null Reference", "");
            if (db.ViewTemplates.IsExist(viewTemplate.Id))
                UpdateViewTemplate(viewTemplate);
            else
                CreateViewTemplate(viewTemplate);
        }

        public void CreateViewTemplate(ViewTemplateDTO viewTemplateDto)
        {
            if (viewTemplateDto == null)
                throw new ValidationException("ViewTemplateDTO Null Reference", "");
            Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplateDTO, ViewTemplate>());
            var viewTemplate = Mapper.Map<ViewTemplateDTO, ViewTemplate>(viewTemplateDto);
            db.ViewTemplates.Create(viewTemplate);
            db.Save();
        }

        public void UpdateViewTemplate(ViewTemplateDTO viewTemplateDto)
        {
            if (viewTemplateDto == null)
                throw new ValidationException("ViewTemplateDTO Null Reference", "");
            if (!db.ViewTemplates.IsExist(viewTemplateDto.Id))
                throw new ValidationException("ViewTemplate Not Found", "");
            Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplateDTO, ViewTemplate>());
            var viewTemplate = Mapper.Map<ViewTemplateDTO, ViewTemplate>(viewTemplateDto);
            db.ViewTemplates.Update(viewTemplate);
            db.Save();
        }

        public void DeleteViewTemplate(int? id)
        {
            if (id == null)
                throw new ValidationException("ViewTemplate Id Not Set", "");
            if (!db.ViewTemplates.IsExist(id.Value))
                throw new ValidationException("ViewTemplate Not Found", "");
            db.ViewTemplates.Delete(id.Value);
            db.Save();
        }
    }
}
