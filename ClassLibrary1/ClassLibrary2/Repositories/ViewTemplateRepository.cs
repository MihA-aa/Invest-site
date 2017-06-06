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
    public class ViewTemplateRepository : GenericRepository<ViewTemplate>, IViewTemplateRepository
    {
        public ViewTemplateRepository(ApplicationContext context) : base(context)
        {
        }

        public bool IsExist(int id)
        {
            return dbSet
                .AsNoTracking()
                .Any(p => p.Id == id);
        }

        public void AddColumnToTemplate(ViewTemplateColumn column, int templateId)
        {
            var template = dbSet.Find(templateId);
            template.Columns.Add(column);
            column.ViewTemplate = template;
            column.ViewTemplateId = templateId;
        }

        public int GetCountColumnInTemplate(int templateId)
        {
            return dbSet.Find(templateId)?.Columns.Count() ?? 0;
        }

        public int GetTemplateIdByColumnId(int columnId)
        {
            return dbSet.FirstOrDefault(x => x.Columns.Any(p => p.Id == columnId))?.Id ?? 0;
        }
    }
}
