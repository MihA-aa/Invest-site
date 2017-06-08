using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Helpers;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ViewTemplateService: IViewTemplateService
    {
        IUnitOfWork db { get; }
        IMapper IMapper { get; }

        public ViewTemplateService(IUnitOfWork uow, IMapper map)
        {
            db = uow;
            IMapper = map;
        }

        public IEnumerable<ViewTemplateDTO> GetViewTemplates()
        {
            return IMapper.Map<IEnumerable<ViewTemplate>, List<ViewTemplateDTO>>(db.ViewTemplates.GetAll());
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
            return IMapper.Map<ViewTemplate, ViewTemplateDTO>(viewTemplate);
        }

        public IEnumerable<ViewTemplateColumnDTO> GetViewTemplateColumns(int? viewTemplateId)
        {
            if (viewTemplateId == null)
                throw new ValidationException("View Template id not set", "");
            var viewTemplate = db.ViewTemplates.Get(viewTemplateId.Value);
            if (viewTemplate == null)
                throw new ValidationException("View Template not found", "");
            return IMapper.Map<IEnumerable<ViewTemplateColumn>, List<ViewTemplateColumnDTO>>(viewTemplate.Columns.ToList());
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
            db.ViewTemplates.Create(IMapper.Map<ViewTemplateDTO, ViewTemplate>(viewTemplateDto));
            db.Save();
        }

        public void UpdateViewTemplate(ViewTemplateDTO viewTemplateDto)
        {
            if (viewTemplateDto == null)
                throw new ValidationException("ViewTemplateDTO Null Reference", "");
            if (!db.ViewTemplates.IsExist(viewTemplateDto.Id))
                throw new ValidationException("ViewTemplate Not Found", "");
            var viewTemplate = IMapper.Map<ViewTemplateDTO, ViewTemplate>(viewTemplateDto);
            AddSortColumnToTemplate(viewTemplate, viewTemplate.SortColumnId);
            db.ViewTemplates.Update(viewTemplate);
            db.Save();
        }

        public void AddSortColumnToTemplate(ViewTemplate template, int? columnId)
        {
            if (template == null)
                throw new ValidationException("Template null Reference", "");
            if (columnId == null)
                return;
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
