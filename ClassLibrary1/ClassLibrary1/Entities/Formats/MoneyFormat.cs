using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstract;
using DAL.Enums;

namespace DAL.Formats
{
    public class MoneyFormat: Format
    {
        public MoneyFormat()
        {
            ColumnFormats = new List<ColumnFormats> { Enums.ColumnFormats.None, Enums.ColumnFormats.Money, Enums.ColumnFormats.Linked };
        }
    }
}
