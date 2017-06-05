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
        void CreateOrUpdateViewTemplateColumn(ViewTemplateColumnDTO viewTemplateColumn, int? templateId);
        void CreateViewTemplateColumn(ViewTemplateColumnDTO viewTemplateColumnDto, int? templateId);
        void UpdateViewTemplateColumn(ViewTemplateColumnDTO viewTemplateColumnDto);
        void DeleteViewTemplateColumn(int? id);
        ViewTemplateColumnDTO GetViewTemplateColumn(int? id);
        IEnumerable<ColumnFormatDTO> GetFormatsByColumnName(string column);

    }
}
