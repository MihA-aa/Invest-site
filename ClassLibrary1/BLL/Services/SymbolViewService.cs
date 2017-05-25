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
    public class SymbolViewService: ISymbolViewService
    {
        IUnitOfWork db { get; }

        public SymbolViewService(IUnitOfWork uow)
        {
            db = uow;
        }

        public SymbolViewDTO GetSymbolViewByName(string name)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<SymbolView, SymbolViewDTO>());
            return Mapper.Map<SymbolView, SymbolViewDTO>(db.SymbolViews.GetSymbolViewByName(name));
        }

        public IEnumerable<string> SearchSymbolsByName(string name)
        {
            return db.SymbolViews.SearchSymbolsViewByName(name);
        }
    }
}
