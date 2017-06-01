using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstract;
using DAL.Enums;

namespace DAL.Formats
{
    public class LineFormat : Format
    {
        public LineFormat()
        {
            ColumnFormats = new List<ColumnFormats> {Enums.ColumnFormats.Linked, Enums.ColumnFormats.None };
        }
    }
}
