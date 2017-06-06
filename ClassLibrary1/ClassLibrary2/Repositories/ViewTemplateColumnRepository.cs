using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using DALEF.EF;

namespace DALEF.Repositories
{
    public class ViewTemplateColumnRepository : GenericRepository<ViewTemplateColumn>, IViewTemplateColumnRepository
    {
        public ViewTemplateColumnRepository(ApplicationContext context) : base(context)
        {
        }

        public bool IsExist(int id)
        {
            return dbSet
                .AsNoTracking()
                .Any(p => p.Id == id);
        }

        public void SortDisplayIndex(int templateId)
        {
            var movedColumns = dbSet
                            .Where(c => (c.ViewTemplateId == templateId))
                            .ToList()
                            .OrderBy(c => c.DisplayIndex)
                            .ToList();

            for (int i = 0; i < movedColumns.Count; i++)
            {
                if(i == 0)
                {
                    movedColumns[i].DisplayIndex = 1;
                }
                else if (movedColumns[i].DisplayIndex -1  > movedColumns[i - 1].DisplayIndex)
                {
                    movedColumns[i].DisplayIndex -= movedColumns[i].DisplayIndex - movedColumns[i - 1].DisplayIndex - 1;
                }
            }
            var s = movedColumns;
        }

        public void UpdateColumnOrder(int id, int fromPosition, int toPosition, string direction, int templateId) //back - вверх
        {
            if (direction == "back")
            {
                var movedColumns = dbSet
                            .Where(c => (toPosition <= c.DisplayIndex && c.DisplayIndex <= fromPosition && c.ViewTemplateId == templateId))
                            .ToList();

                foreach (var column in movedColumns)
                {
                    column.DisplayIndex++;
                }
            }
            else
            {
                var movedColumns = dbSet
                            .Where(c => (fromPosition <= c.DisplayIndex && c.DisplayIndex <= toPosition && c.ViewTemplateId == templateId))
                            .ToList();
                foreach (var column in movedColumns)
                {
                    column.DisplayIndex--;
                }
            }

            dbSet.Find(id).DisplayIndex = toPosition;
        }
    }
}
