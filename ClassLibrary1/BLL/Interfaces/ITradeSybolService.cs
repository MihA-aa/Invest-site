using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITradeSybolService
    {
        decimal GetPriceForDate(DateTime date, int symbolId);
    }
}
