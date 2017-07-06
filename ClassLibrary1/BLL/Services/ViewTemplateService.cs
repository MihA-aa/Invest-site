using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.DTO.Enums;
using BLL.Helpers;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ViewTemplateService: BaseService, IViewTemplateService
    {
        ICustomerService customerService { get; }
        IRecordService recordService { get; }
        public ViewTemplateService(IUnitOfWork uow, IValidateService vd, IMapper map, ICustomerService cs,
                                    IRecordService rs) : base(uow, vd, map)
        {
            customerService = cs;
            recordService = rs;
        }

        public IEnumerable<ViewTemplateDTO> GetViewTemplates()
        {
            return IMapper.Map<IEnumerable<ViewTemplate>, List<ViewTemplateDTO>>(db.ViewTemplates.GetAll());
        }
        public IEnumerable<ViewTemplateDTO> GetViewTemplatesForUser(string id)
        {
            var profile = db.Profiles.Get(id);
            if (profile == null)
                throw new ValidationException(Resource.Resource.ProfileNotFound, "");
            return IMapper.Map<IEnumerable<ViewTemplate>, List<ViewTemplateDTO>>(profile.Customer?.ViewTemplates);
        }

        public bool CheckAccess(string userId, int? viewTemplateId)
        {
            if (viewTemplateId == null)
                throw new ValidationException(Resource.Resource.ViewTemplateIdNotSet, "");
            if (userId == null)
                throw new ValidationException(Resource.Resource.ProfileIdNotSet, "");
            var profile = db.Profiles.Get(userId);
            var viewTemplates = profile?.Customer?.ViewTemplates;
            if (viewTemplates?.FirstOrDefault(p => p.Id == viewTemplateId) != null)
                return true;
            return false;
        }

        public string GetNameByTemplateId(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.ViewTemplateIdNotSet, "");
            var viewTemplate = db.ViewTemplates.Get(id.Value);
            if (viewTemplate == null)
                throw new ValidationException(Resource.Resource.ViewTemplateNotFound, "");
            return viewTemplate.Name;
        }

        public ViewTemplateDTO GetViewTemplate(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.ViewTemplateIdNotSet, "");
            var viewTemplate = db.ViewTemplates.Get(id.Value);
            if (viewTemplate == null)
                throw new ValidationException(Resource.Resource.ViewTemplateNotFound, "");
            return IMapper.Map<ViewTemplate, ViewTemplateDTO>(viewTemplate);
        }

        public IEnumerable<ViewTemplateColumnDTO> GetViewTemplateColumns(int? viewTemplateId)
        {
            if (viewTemplateId == null)
                throw new ValidationException(Resource.Resource.ViewTemplateIdNotSet, "");
            var viewTemplate = db.ViewTemplates.Get(viewTemplateId.Value);
            if (viewTemplate == null)
                throw new ValidationException(Resource.Resource.ViewTemplateNotFound, "");
            return IMapper.Map<IEnumerable<ViewTemplateColumn>, List<ViewTemplateColumnDTO>>(viewTemplate.Columns.ToList());
        }

        public void CreateOrUpdateViewTemplate(ViewTemplateDTO viewTemplate, string userId)
        {
            if (viewTemplate == null)
                throw new ValidationException(Resource.Resource.ViewTemplateNullReference, "");
            if (db.ViewTemplates.IsExist(viewTemplate.Id))
                UpdateViewTemplate(viewTemplate, userId);
            else
                CreateViewTemplate(viewTemplate, userId);
        }

        public void CreateViewTemplate(ViewTemplateDTO viewTemplateDto, string userId)
        {
            var transaction = db.BeginTransaction();
            try
            {
                if (viewTemplateDto == null)
                    throw new ValidationException(Resource.Resource.ViewTemplateNullReference, "");
                var viewTemplate = IMapper.Map<ViewTemplateDTO, ViewTemplate>(viewTemplateDto);
                var customer = customerService.GetCustomerByProfileId(userId);
                viewTemplate.Customer = customer;
                customer.ViewTemplates.Add(viewTemplate);
                db.ViewTemplates.Create(viewTemplate);
                db.Save();

                recordService.CreateRecord(EntitiesDTO.ViewTemplate, OperationsDTO.Create, userId, viewTemplate.Id, true);
                db.Commit(transaction);
            }
            catch (Exception ex)
            {
                db.RollBack(transaction);
                recordService.CreateRecord(EntitiesDTO.ViewTemplate, OperationsDTO.Create, userId, 0, false);
                throw ex;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public void UpdateViewTemplate(ViewTemplateDTO viewTemplateDto, string userId)
        {
            var transaction = db.BeginTransaction();
            try
            {
                if (viewTemplateDto == null)
                    throw new ValidationException(Resource.Resource.ViewTemplateNullReference, "");
                if (!db.ViewTemplates.IsExist(viewTemplateDto.Id))
                    throw new ValidationException(Resource.Resource.ViewTemplateNotFound, "");
                var viewTemplate = IMapper.Map<ViewTemplateDTO, ViewTemplate>(viewTemplateDto);
                AddSortColumnToTemplate(viewTemplate, viewTemplate.SortColumnId);
                db.ViewTemplates.Update(viewTemplate);
                db.Save();

                recordService.CreateRecord(EntitiesDTO.ViewTemplate, OperationsDTO.Update, userId, viewTemplate.Id, true);
                db.Commit(transaction);
            }
            catch (Exception ex)
            {
                db.RollBack(transaction);
                recordService.CreateRecord(EntitiesDTO.ViewTemplate, OperationsDTO.Update, userId, viewTemplateDto?.Id ?? 0, false);
                throw ex;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public void AddSortColumnToTemplate(ViewTemplate template, int? columnId)
        {
            if (template == null)
                throw new ValidationException(Resource.Resource.ViewTemplateNullReference, "");
            if (columnId == null)
                return;
            var column = db.ViewTemplateColumns.Get(columnId.Value);
            if (column == null)
                throw new ValidationException(Resource.Resource.ViewTemplateColumnNotFound, "");
            template.SortColumn = column;
        }

        public void DeleteViewTemplate(int? id, string userId)
        {
            var transaction = db.BeginTransaction();
            try
            {
                if (id == null)
                    throw new ValidationException(Resource.Resource.ViewTemplateIdNotSet, "");
                if (!db.ViewTemplates.IsExist(id.Value))
                    throw new ValidationException(Resource.Resource.ViewTemplateNotFound, "");
                db.ViewTemplates.Delete(id.Value);
                db.Save();

                recordService.CreateRecord(EntitiesDTO.ViewTemplate, OperationsDTO.Delete, userId, id.Value, true);
                db.Commit(transaction);
            }
            catch (Exception ex)
            {
                db.RollBack(transaction);
                recordService.CreateRecord(EntitiesDTO.ViewTemplate, OperationsDTO.Delete, userId, id ?? 0, false);
                throw ex;
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
