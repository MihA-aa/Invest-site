using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Views;

namespace DAL.Interfaces
{
    public interface ISymbolViewRepository
    {
        IEnumerable<SymbolView> Find(Func<SymbolView, bool> predicate);
        SymbolView Get(int id);
        Task<SymbolView> GetAsync(int id);
        IEnumerable<string> SearchSymbolsViewByName(string name);
        SymbolView GetSymbolViewByName(string name);
    }
}
