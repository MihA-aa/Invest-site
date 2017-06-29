using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IViewTemplateColumnService
    {
        void CreateOrUpdateViewTemplateColumn(ViewTemplateColumnDTO viewTemplateColumn, int? templateId, string userId);
        void CreateViewTemplateColumn(ViewTemplateColumnDTO viewTemplateColumnDto, int? templateId, string userId);
        void UpdateViewTemplateColumn(ViewTemplateColumnDTO viewTemplateColumnDto, string userId);
        void DeleteViewTemplateColumn(int? id, string userId);
        ViewTemplateColumnDTO GetViewTemplateColumn(int? id);
        IEnumerable<ColumnFormatDTO> GetFormatsByColumnName(string column);
        void UpdateColumnOrder(int id, int fromPosition, int toPosition, string direction);
        int GetDisplayIndexForViewTemplateColumnById(int? id);

    }
}
