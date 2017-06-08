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
            var viewTemplates = db.ViewTemplates.GetAll();
            if (viewTemplates == null)
            {
                return null;
            }
            Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplate, ViewTemplateDTO>());
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
            Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplateColumn, ViewTemplateColumnDTO>()
            .ForMember("ColumnName", opt => opt.MapFrom(src => src.Column.Name)));
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
            AddSortColumnToTemplate(viewTemplate, viewTemplate.SortColumnId);
            db.ViewTemplates.Update(viewTemplate);
            db.Save();
        }

        public void AddSortColumnToTemplate(ViewTemplate template, int? columnId)
        {
            if (template == null)
                throw new ValidationException("Template null Reference", "");
            if (columnId == null)
                throw new ValidationException("Column Id Not Set", "");
            var column = db.ViewTemplateColumns.Get(columnId.Value);
            if (column == null)
                throw new ValidationException("Column Not Found", "");
            template.SortColumn = column;
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
