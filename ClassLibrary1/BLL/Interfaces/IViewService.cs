using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IViewService
    {
        IEnumerable<ViewDTO> GetViews();
        IEnumerable<ViewDTO> GetViewsForUser(string id);
        ViewDTO GetView(int? id);
        void CreateOrUpdateView(ViewDTO view, string userId);
        void CreateView(ViewDTO viewDto, string userId);
        void UpdateView(ViewDTO viewDto);
        void DeleteView(int? id);
        void AddViewTemplateToView(View view, int? ViewTemplateId);
        void AddPortfolioToView(View view, int? PortfolioId);
        bool CheckAccess(string userId, int? viewId);
    }
}
