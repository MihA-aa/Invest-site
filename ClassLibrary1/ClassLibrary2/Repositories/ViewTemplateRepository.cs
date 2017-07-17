using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using NHibernate;
using NHibernate.Linq;

namespace DALEF.Repositories
{
    public class ViewTemplateRepository : GenericRepository<ViewTemplate>, IViewTemplateRepository
    {
        public ViewTemplateRepository(ISession session) : base(session)
        {
        }

        public bool IsExist(int id)
        {
            return Session.Query<ViewTemplate>()
                .Any(p => p.Id == id);
        }

        public void AddColumnToTemplate(ViewTemplateColumn column, int templateId)
        {
            var template = Session.Get<ViewTemplate>(templateId);
            template.Columns.Add(column);
            column.ViewTemplate = template;
            column.ViewTemplateId = templateId;
        }

        public int GetCountColumnInTemplate(int templateId)
        {
            return Session.Get<ViewTemplate>(templateId)?.Columns.Count() ?? 0;
        }

        public ViewTemplate GetViewTemplate(int id)
        {
            return Session.Get<ViewTemplate>(id);
        }

        public int GetTemplateIdByColumnId(int columnId)
        {
            return Session.Query<ViewTemplate>().FirstOrDefault(x => x.Columns.Any(p => p.Id == columnId))?.Id ?? 0;
        }
    }
}
