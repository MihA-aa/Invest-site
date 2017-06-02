using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Enums;
using DAL.Enums;

namespace BLL.DTO
{
    public class ViewTemplateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TemplatePositionsDTO Positions { get; set; }
        public bool ShowPortfolioStats { get; set; }
        public SortingDTO SortOrder { get; set; }
        public int? SortColumnId { get; set; }
    }
}
