using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Enums;
using DAL.Entities;
using DAL.Enums;
using AutoMapper;

namespace BLL.DTO
{
    public class ViewTemplateColumnDTO 
    {
        public int Id { get; set; }
        public int DisplayIndex { get; set; }
        public string Name { get; set; }
        public string ColumnName { get; set; }
        public int? ViewTemplateId { get; set; }
        public int? ColumnFormatId { get; set; }
        public int? ColumnId { get; set; }
        public string FormatName { get; set; }
    }
}
