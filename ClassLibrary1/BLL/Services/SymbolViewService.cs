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
        IMapper IMapper { get; }

        public SymbolViewService(IUnitOfWork uow, IMapper map)
        {
            db = uow;
            IMapper = map;
        }

        public SymbolViewDTO GetSymbolViewByName(string name)
        {
            return IMapper.Map<SymbolView, SymbolViewDTO>(db.SymbolViews.GetSymbolViewByName(name));
        }

        public IEnumerable<string> SearchSymbolsByName(string name)
        {
            return db.SymbolViews.SearchSymbolsViewByName(name);
        }
    }
}
