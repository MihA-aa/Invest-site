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
        public int DisplayIndex { get; set; }
        public string Name { get; set; }
        public int? ViewTemplateId { get; set; }
        public int? FormatId { get; set; }
    }
}
