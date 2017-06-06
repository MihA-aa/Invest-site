using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.DTO.Enums;

namespace PL.Models
{
    public class ViewTemplateColumnModel
    {
        public int Id { get; set; }

        public int? DisplayIndex { get; set; }
        public string DT_RowId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Column")]
        public string ColumnName { get; set; }
        

        public string ViewTemplate { get; set; }

        public int? ViewTemplateId { get; set; }

        [Required]
        [Display(Name = "Format")]
        public int? FormatId { get; set; }
    }
}