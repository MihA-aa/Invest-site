using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Entities
{
    public class ViewTemplateColumn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ColumnNames Column { get; set; }
        public ColumnFormats Format { get; set; }
        public virtual ViewTemplate ViewTemplate { get; set; }
    }
}
