using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Enums;

namespace DAL.Abstract
{
    public class Format
    {
        public virtual ICollection<ColumnFormats> ColumnFormats { get; set; }
        public Format()
        {
            ColumnFormats = new List<ColumnFormats> {};
        }
    }
}
