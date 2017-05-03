using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Enums;

namespace BLL.DTO
{
    class ViewTemplateColumnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ColumnNames Column { get; set; }
        public ColumnFormats Format { get; set; }
    }
}
