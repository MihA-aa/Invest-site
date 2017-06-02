using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.DTO.Enums;

namespace PL.Models
{
    public class ViewTemplateModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Show Positions")]
        public TemplatePositionsDTO Positions { get; set; }
        
        [Display(Name = "Show Portfolio Stats")]
        public bool ShowPortfolioStats { get; set; }

        [Display(Name = "Sort Order")]
        public SortingDTO SortOrder { get; set; }

        [Display(Name = "Sort Column")]
        public SelectList Columns { get; set; }

        public int? SortColumnId { get; set; }
    }
}