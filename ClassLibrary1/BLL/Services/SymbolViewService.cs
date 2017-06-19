using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Entities.Views;
using DAL.Interfaces;

namespace BLL.Services
{
    public class SymbolViewService:BaseService, ISymbolViewService
    {
        public SymbolViewService(IUnitOfWork uow, IValidateService vd, IMapper map) : base(uow, vd, map) { }

        public SymbolViewDTO GetSymbolViewByName(string name)
        {
            return IMapper.Map<SymbolView, SymbolViewDTO>(db.SymbolViews.GetSymbolViewByName(name));
        }

        public string GetCurrencySymbolViewBySymbolId(int id)
        {
            return db.SymbolViews.Get(id)?.CurrencySymbol;
        }

        public IEnumerable<string> SearchSymbolsByName(string name)
        {
            return db.SymbolViews.SearchSymbolsViewByName(name);
        }
    }
}
