using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IViewTemplateColumnRepository : IRepository<ViewTemplateColumn>
    {
        bool IsExist(int id);
        void UpdateColumnOrder(int id, int fromPosition, int toPosition, string direction, int templateId);
        void SortDisplayIndex(int templateId);
    }
}
