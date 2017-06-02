using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Enums;
using DAL.Enums;

namespace BLL.DTO
{
    public class ViewTemplateColumnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ColumnNamesDTO Column { get; set; }
    }
}
