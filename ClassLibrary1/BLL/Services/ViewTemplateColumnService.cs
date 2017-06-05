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
    public class ViewTemplateColumnService: IViewTemplateColumnService
    {
        IUnitOfWork db { get; }
        IValidateService validateService { get; }
        public ViewTemplateColumnService(IUnitOfWork uow, IValidateService vd)
        {
            db = uow;
            validateService = vd;
        }

        public ViewTemplateColumnDTO GetViewTemplateColumn(int? id)
        {
            if (id == null)
                throw new ValidationException("viewTemplateColumnDTO Id Not Set", "");
            var viewTemplateColumn = db.ViewTemplateColumns.Get(id.Value);
            if (viewTemplateColumn == null)
                throw new ValidationException("viewTemplateColumnDTO Not Found", "");
            Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplateColumn, ViewTemplateColumnDTO>());
            return Mapper.Map<ViewTemplateColumn, ViewTemplateColumnDTO>(viewTemplateColumn);
        }

        public void CreateOrUpdateViewTemplateColumn(ViewTemplateColumnDTO viewTemplateColumn, int? templateId)
        {
            if (viewTemplateColumn == null)
                throw new ValidationException("viewTemplateColumnDTO Null Reference", "");
            if (db.ViewTemplateColumns.IsExist(viewTemplateColumn.Id))
                UpdateViewTemplateColumn(viewTemplateColumn);
            else
                CreateViewTemplateColumn(viewTemplateColumn, templateId);
        }

        public void CreateViewTemplateColumn(ViewTemplateColumnDTO viewTemplateColumnDto, int? templateId)
        {
            if (viewTemplateColumnDto == null)
                throw new ValidationException("ViewTemplateColumnDTO Null Reference", "");
            Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplateColumnDTO, ViewTemplateColumn>());
            var viewTemplateColumn = Mapper.Map<ViewTemplateColumnDTO, ViewTemplateColumn>(viewTemplateColumnDto);
            db.ViewTemplateColumns.Create(viewTemplateColumn);
            AddColumnToTemplate(viewTemplateColumn, templateId);
            db.Save();
        }

        public void AddColumnToTemplate(ViewTemplateColumn column, int? templateId)
        {
            if (column == null)
                throw new ValidationException("column null Reference", "");
            if (templateId == null)
                throw new ValidationException("Template Id Not Set", "");
            if (!db.ViewTemplates.IsExist(templateId.Value))
                throw new ValidationException("Template Not Found", "");
            db.ViewTemplates.AddColumnToTemplate(column, templateId.Value);
        }

        public void UpdateViewTemplateColumn(ViewTemplateColumnDTO viewTemplateColumnDto)
        {
            if (viewTemplateColumnDto == null)
                throw new ValidationException("ViewTemplateColumnDTO Null Reference", "");
            if (!db.ViewTemplateColumns.IsExist(viewTemplateColumnDto.Id))
                throw new ValidationException("ViewTemplateColumnDTO Not Found", "");
            Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplateColumnDTO, ViewTemplateColumn>());
            var viewTemplateColumn = Mapper.Map<ViewTemplateColumnDTO, ViewTemplateColumn>(viewTemplateColumnDto);
            db.ViewTemplateColumns.Update(viewTemplateColumn);
            db.Save();
        }

        public void DeleteViewTemplateColumn(int? id)
        {
            if (id == null)
                throw new ValidationException("viewTemplateColumn Id Not Set", "");
            if (!db.ViewTemplateColumns.IsExist(id.Value))
                throw new ValidationException("viewTemplateColumn Not Found", "");
            db.ViewTemplateColumns.Delete(id.Value);
            db.Save();
        }
        public IEnumerable<ColumnFormatDTO> GetFormatsByColumnName(string column)
        {
            var formats = db.Columns.GetFormatsByColumnName(column).ColumnFormats;
            Mapper.Initialize(cfg => cfg.CreateMap<ColumnFormat, ColumnFormatDTO>());
            return Mapper.Map<IEnumerable<ColumnFormat>, List<ColumnFormatDTO>>(formats);
        }
    }
}
