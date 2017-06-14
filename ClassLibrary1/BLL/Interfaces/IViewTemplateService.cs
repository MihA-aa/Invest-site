using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IViewTemplateService
    {
        IEnumerable<ViewTemplateDTO> GetViewTemplates();
        ViewTemplateDTO GetViewTemplate(int? id);
        string GetNameByTemplateId(int? id);
        IEnumerable<ViewTemplateColumnDTO> GetViewTemplateColumns(int? viewTemplateId);
        void CreateOrUpdateViewTemplate(ViewTemplateDTO viewTemplate, string userId);
        void CreateViewTemplate(ViewTemplateDTO viewTemplateDto, string userId);
        void UpdateViewTemplate(ViewTemplateDTO viewTemplateDto);
        void AddSortColumnToTemplate(ViewTemplate template, int? columnId);
        void DeleteViewTemplate(int? id);
        IEnumerable<ViewTemplateDTO> GetViewTemplatesForUser(string id);
    }
}
