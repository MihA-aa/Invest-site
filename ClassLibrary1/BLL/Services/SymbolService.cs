using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class SymbolService : ISymbolService
    {
        IUnitOfWork db { get; }

        public SymbolService(IUnitOfWork uow)
        {
            db = uow;
        }

        public IEnumerable<SymbolDTO> GetSymbols()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Symbol, SymbolDTO>());
            return Mapper.Map<IEnumerable<Symbol>, List<SymbolDTO>>(db.Symbols.GetAll());
        }

        public SymbolDTO GetSymbolByName(string name)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Symbol, SymbolDTO>());
            return Mapper.Map<Symbol, SymbolDTO>(db.Symbols.Find(s => s.Name == name).FirstOrDefault());
        }

        public IEnumerable<string> SearchSymbolsByName(string name)
        {
            return db.Symbols.SearchSymbolsByName(name);
        }
    }
}
