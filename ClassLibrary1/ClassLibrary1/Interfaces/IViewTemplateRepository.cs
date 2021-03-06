﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IViewTemplateRepository : IRepository<ViewTemplate>
    {
        bool IsExist(int id);
        void AddColumnToTemplate(ViewTemplateColumn column, int templateId);
        int GetCountColumnInTemplate(int templateId);
        int GetTemplateIdByColumnId(int columnId);
        ViewTemplate GetViewTemplate(int id);
    }
}
