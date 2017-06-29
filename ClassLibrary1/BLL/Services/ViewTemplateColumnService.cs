using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.DTO.Enums;
using BLL.Helpers;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ViewTemplateColumnService: BaseService, IViewTemplateColumnService
    {
        IRecordService recordService { get; }

        public ViewTemplateColumnService(IUnitOfWork uow, IValidateService vd, IMapper map, 
                                                    IRecordService rs) : base(uow, vd, map)
        {
            recordService = rs;
        }
        
        public ViewTemplateColumnDTO GetViewTemplateColumn(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.ViewTemplateColumnIdNotSet, "");
            var viewTemplateColumn = db.ViewTemplateColumns.Get(id.Value);
            if (viewTemplateColumn == null)
                throw new ValidationException(Resource.Resource.ViewTemplateColumnNotFound, "");
            return IMapper.Map<ViewTemplateColumn, ViewTemplateColumnDTO>(viewTemplateColumn);
        }

        public void CreateOrUpdateViewTemplateColumn(ViewTemplateColumnDTO viewTemplateColumn, int? templateId, string userId)
        {
            if (viewTemplateColumn == null)
                throw new ValidationException(Resource.Resource.ViewTemplateColumnNullReference, "");
            if (db.ViewTemplateColumns.IsExist(viewTemplateColumn.Id))
                UpdateViewTemplateColumn(viewTemplateColumn, userId);
            else
                CreateViewTemplateColumn(viewTemplateColumn, templateId, userId);
        }

        public void CreateViewTemplateColumn(ViewTemplateColumnDTO viewTemplateColumnDto, int? templateId, string userId)
        {
            int recordId = recordService.CreateRecord(EntitiesDTO.ViewTemplateColumn, OperationsDTO.Create, userId);
            if (viewTemplateColumnDto == null)
                throw new ValidationException(Resource.Resource.ViewTemplateColumnNullReference, "");
            Mapper.Initialize(cfg => cfg.CreateMap<ViewTemplateColumnDTO, ViewTemplateColumn>()
            .ForMember("DisplayIndex", opt => opt.MapFrom(src => db.ViewTemplates.GetCountColumnInTemplate(templateId.Value) + 1)));
            var viewTemplateColumn = Mapper.Map<ViewTemplateColumnDTO, ViewTemplateColumn>(viewTemplateColumnDto);
            db.ViewTemplateColumns.Create(viewTemplateColumn);
            AddColumnToViewTemplateColumn(viewTemplateColumn, viewTemplateColumnDto.ColumnName);
            AddColumnToTemplate(viewTemplateColumn, templateId);
            ApplyFormatToColumn(viewTemplateColumn, viewTemplateColumnDto.ColumnFormatId);
            db.Save();
            recordService.SetEntityId(viewTemplateColumn.Id, recordId);
            recordService.EstablishSuccess(recordId);
        }

        public void AddColumnToViewTemplateColumn(ViewTemplateColumn viewTemplateColumn, string columnName)
        {
            if (viewTemplateColumn == null)
                throw new ValidationException(Resource.Resource.ViewTemplateColumnNullReference, "");
            if (columnName == null)
                throw new ValidationException(Resource.Resource.ViewTemplateColumnNameNotSet, "");
            var column = db.Columns.GetColumnByColumnName(columnName);
            if (column == null)
                throw new ValidationException(Resource.Resource.ViewTemplateColumnNotFound, "");
            viewTemplateColumn.ColumnId = column.Id;
            viewTemplateColumn.Column = column;
        }

        public void ApplyFormatToColumn(ViewTemplateColumn column, int? FormatId)
        {
            if (column == null)
                throw new ValidationException(Resource.Resource.ViewTemplateColumnNullReference, "");
            if (FormatId == null)
                throw new ValidationException(Resource.Resource.FormatIdNotSet, "");
            var columnFormat = db.ColumnFormats.Get(FormatId.Value);
            if (columnFormat == null)
                throw new ValidationException(Resource.Resource.FormatNotFound, "");
            column.ColumnFormatId = columnFormat.Id;
            column.ColumnFormat = columnFormat;
        }
        
        public void AddColumnToTemplate(ViewTemplateColumn column, int? templateId)
        {
            if (column == null)
                throw new ValidationException(Resource.Resource.ViewTemplateColumnNullReference, "");
            if (templateId == null)
                throw new ValidationException(Resource.Resource.ViewTemplateIdNotSet, "");
            if (!db.ViewTemplates.IsExist(templateId.Value))
                throw new ValidationException(Resource.Resource.ViewTemplateNotFound, "");
            db.ViewTemplates.AddColumnToTemplate(column, templateId.Value);
        }

        public void UpdateViewTemplateColumn(ViewTemplateColumnDTO viewTemplateColumnDto, string userId)
        {
            int recordId = recordService.CreateRecord(EntitiesDTO.ViewTemplateColumn, OperationsDTO.Update, userId);
            if (viewTemplateColumnDto == null)
                throw new ValidationException(Resource.Resource.ViewTemplateColumnNullReference, "");
            if (!db.ViewTemplateColumns.IsExist(viewTemplateColumnDto.Id))
                throw new ValidationException(Resource.Resource.ViewTemplateColumnNotFound, "");
            var viewTemplateColumn = IMapper.Map<ViewTemplateColumnDTO, ViewTemplateColumn>(viewTemplateColumnDto);
            AddColumnToViewTemplateColumn(viewTemplateColumn, viewTemplateColumnDto.ColumnName);
            ApplyFormatToColumn(viewTemplateColumn, viewTemplateColumnDto.ColumnFormatId);
            db.ViewTemplateColumns.Update(viewTemplateColumn);
            db.Save();
            recordService.EstablishSuccess(recordId);
        }

        public void DeleteViewTemplateColumn(int? id, string userId)
        {
            int recordId = recordService.CreateRecord(EntitiesDTO.ViewTemplateColumn, OperationsDTO.Delete, userId);
            if (id == null)
                throw new ValidationException(Resource.Resource.ViewTemplateColumnIdNotSet, "");
            if (!db.ViewTemplateColumns.IsExist(id.Value))
                throw new ValidationException(Resource.Resource.ViewTemplateColumnNotFound, "");
            int templateId = db.ViewTemplates.GetTemplateIdByColumnId(id.Value);
            db.ViewTemplateColumns.Delete(id.Value);
            db.Save();
            db.ViewTemplateColumns.SortDisplayIndex(templateId);
            db.Save();
            recordService.EstablishSuccess(recordId);
        }
        public IEnumerable<ColumnFormatDTO> GetFormatsByColumnName(string column)
        {
            var formats = db.Columns.GetFormatsByColumnName(column).ColumnFormats;
            return IMapper.Map<IEnumerable<ColumnFormat>, List<ColumnFormatDTO>>(formats);
        }

        public int GetDisplayIndexForViewTemplateColumnById(int? id)
        {
            var column = db.ViewTemplateColumns.Get(id.Value);
            if (column == null)
                throw new ValidationException(Resource.Resource.ViewTemplateColumnNotFound, "");
            return column.DisplayIndex;
        }

        public void UpdateColumnOrder(int id, int fromPosition, int toPosition, string direction)
        {
            int templateId = db.ViewTemplates.GetTemplateIdByColumnId(id);
            db.ViewTemplateColumns.UpdateColumnOrder(id, fromPosition, toPosition, direction, templateId);
            db.ViewTemplateColumns.SortDisplayIndex(templateId);
            db.Save();
        }
    }
}
