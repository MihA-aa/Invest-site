using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using BLL.Helpers;

namespace BLL.Services
{
    public class ViewService: IViewService
    {
        IUnitOfWork db { get; }
        IValidateService validateService { get; }

        public ViewService(IUnitOfWork uow, IValidateService vd)
        {
            db = uow;
            validateService = vd;
        }

        public IEnumerable<ViewDTO> GetViews()
        {
            var views = db.Views.GetAll();
            if (views == null)
            {
                return null;
            }
            Mapper.Initialize(cfg => cfg.CreateMap<View, ViewDTO>());
            return Mapper.Map<IEnumerable<View>, List<ViewDTO>>(views);
        }

        public ViewDTO GetView(int? id)
        {
            if (id == null)
                throw new ValidationException("View Id Not Set", "");
            var view = db.Views.Get(id.Value);
            if (view == null)
                throw new ValidationException("View Not Found", "");
            Mapper.Initialize(cfg => cfg.CreateMap<View, ViewDTO>());
            return Mapper.Map<View, ViewDTO>(view);
        }
        
        public void CreateOrUpdateView(ViewDTO view)
        {
            if (view == null)
                throw new ValidationException("ViewDTO Null Reference", "");
            if (db.Views.IsExist(view.Id))
                UpdateView(view);
            else
                CreateView(view);
        }

        public void CreateView(ViewDTO viewDto)
        {
            if (viewDto == null)
                throw new ValidationException("ViewDTO Null Reference", "");
            validateService.Validate(viewDto);
            Mapper.Initialize(cfg => cfg.CreateMap<ViewDTO, View>());
            var view = Mapper.Map<ViewDTO, View>(viewDto);
            AddViewTemplateToView(view, view.ViewTemplateId);
            db.Views.Create(view);
            db.Save();
        }

        public void UpdateView(ViewDTO viewDto)
        {
            if (viewDto == null)
                throw new ValidationException("ViewDTO Null Reference", "");
            if (!db.Views.IsExist(viewDto.Id))
                throw new ValidationException("View Not Found", "");
            validateService.Validate(viewDto);
            Mapper.Initialize(cfg => cfg.CreateMap<ViewDTO, View>());
            var view = Mapper.Map<ViewDTO, View>(viewDto);
            AddViewTemplateToView(view, view.ViewTemplateId);
            db.Views.Update(view);
            db.Save();
        }

        public void DeleteView(int? id)
        {
            if (id == null)
                throw new ValidationException("View Id Not Set", "");
            if (!db.Views.IsExist(id.Value))
                throw new ValidationException("View Not Found", "");
            db.Views.Delete(id.Value);
            db.Save();
        }

        public void AddViewTemplateToView(View view, int? ViewTemplateId)
        {
            if (view == null)
                throw new ValidationException("View null Reference", "");
            if (ViewTemplateId == null)
                throw new ValidationException("ViewTemplate Id Not Set", "");
            var viewTemplate = db.ViewTemplates.Get(ViewTemplateId.Value);
            if (viewTemplate == null)
                throw new ValidationException("ViewTemplate Not Found", "");
            view.ViewTemplate = viewTemplate;
        }
        public void AddPortfolioToView(View view, int? PortfolioId)
        {
            if (view == null)
                throw new ValidationException("View null Reference", "");
            if (PortfolioId == null)
                throw new ValidationException("Portfolio Id Not Set", "");
            var portfolio = db.Portfolios.Get(PortfolioId.Value);
            if (portfolio == null)
                throw new ValidationException("Portfolio Not Found", "");
            view.Portfolio = portfolio;
        }

    }
}
