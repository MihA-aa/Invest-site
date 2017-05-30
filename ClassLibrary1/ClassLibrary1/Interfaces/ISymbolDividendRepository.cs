using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Views;

namespace DAL.Interfaces
{
    public interface ISymbolDividendRepository
    {
        SymbolDividend Get(int id);
        decimal GetDividendsInDateInterval(DateTime dateFrom, DateTime dateTo, int symbolId);
    }
}
