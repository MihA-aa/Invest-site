using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.DTO.Enums;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using BLL.Helpers;

namespace BLL.Services
{
    public class ViewService: BaseService, IViewService
    {
        ICustomerService customerService { get; }
        ITransactionService transactionService { get; }

        public ViewService(IUnitOfWork uow, IValidateService vd, IMapper map, ICustomerService cs,
                           ITransactionService ts) : base(uow, vd, map)
        {
            customerService = cs;
            transactionService = ts;
        }

        public IEnumerable<ViewDTO> GetViews()
        {
            return IMapper.Map<IEnumerable<ViewForTable>, List<ViewDTO>>(db.Views.GetAll());
        }

        public IEnumerable<ViewDTO> GetViewsForUser(string id)
        {
            var profile = db.Profiles.Get(id);
            if (profile == null)
                throw new ValidationException(Resource.Resource.ProfileNotFound, "");
            return IMapper.Map<IEnumerable<ViewForTable>, List<ViewDTO>>(profile.Customer?.Views);
        }

        public bool CheckAccess(string userId, int? viewId)
        {
            if (viewId == null)
                throw new ValidationException(Resource.Resource.ViewIdNotSet, "");
            if (userId == null)
                throw new ValidationException(Resource.Resource.ProfileIdNotSet, "");

            var profile = db.Profiles.Get(userId);
            var views = profile?.Customer?.Views;
            if (views?.FirstOrDefault(p => p.Id == viewId) != null)
                return true;
            return false;
        }

        public ViewDTO GetView(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.ViewIdNotSet, "");
            if (!db.Views.IsExist(id.Value))
                throw new ValidationException(Resource.Resource.ViewNotFound, "");
            var view = db.Views.Get(id.Value);
            return IMapper.Map<ViewForTable, ViewDTO>(view);
        }
        
        public void CreateOrUpdateView(ViewDTO view, string userId)
        {
            if (view == null)
                throw new ValidationException(Resource.Resource.ViewNullReference, "");
            if (db.Views.IsExist(view.Id))
                UpdateView(view);
            else
                CreateView(view, userId);
        }

        public void CreateView(ViewDTO viewDto, string userId)
        {
            if (viewDto == null)
                throw new ValidationException(Resource.Resource.ViewNullReference, "");
            validateService.Validate(viewDto);

            var view = IMapper.Map<ViewDTO, ViewForTable>(viewDto);
            AddViewTemplateToView(view, view.ViewTemplateId);
            var customer = customerService.GetCustomerByProfileId(userId);
            view.Customer = customer;
            customer.Views.Add(view);
            db.Views.Create(view);
        }

        public void UpdateView(ViewDTO viewDto)
        {
            if (viewDto == null)
                throw new ValidationException(Resource.Resource.ViewNullReference, "");
            if (!db.Views.IsExist(viewDto.Id))
                throw new ValidationException(Resource.Resource.ViewNotFound, "");
            validateService.Validate(viewDto);

            var view = IMapper.Map<ViewDTO, ViewForTable>(viewDto);
            var viewFromDb = db.Views.Get(viewDto.Id);
            view.Customer = viewFromDb.Customer;
            AddViewTemplateToView(view, view.ViewTemplateId);

            db.Views.Update(view);
        }

        public void DeleteView(int? id)
        {
            if (id == null)
                throw new ValidationException(Resource.Resource.ViewIdNotSet, "");
            if (!db.Views.IsExist(id.Value))
                throw new ValidationException(Resource.Resource.ViewNotFound, "");
            db.Views.Delete(id.Value);
        }

        public void AddViewTemplateToView(ViewForTable view, int? ViewTemplateId)
        {
            if (view == null)
                throw new ValidationException(Resource.Resource.ViewNullReference, "");
            if (ViewTemplateId == null)
                throw new ValidationException(Resource.Resource.ViewTemplateIdNotSet, "");
            var viewTemplate = db.ViewTemplates.Get(ViewTemplateId.Value);
            if (viewTemplate == null)
                throw new ValidationException(Resource.Resource.ViewTemplateNotFound, "");
            view.ViewTemplate = viewTemplate;
        }
    }
}
