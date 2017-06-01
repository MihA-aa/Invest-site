using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Format
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ColumnFormat> ColumnFormats { get; set; }
        public virtual ICollection<ViewTemplateColumn> ViewTemplateColumns { get; set; }
        public Format()
        {
            ColumnFormats = new List<ColumnFormat>();
            ViewTemplateColumns = new List<ViewTemplateColumn>();
        }
    }
}
