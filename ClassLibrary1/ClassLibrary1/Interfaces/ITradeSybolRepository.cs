using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITradeSybolRepository
    {
        decimal GetPriceForDate(DateTime date, int symbolId);
    }
}
