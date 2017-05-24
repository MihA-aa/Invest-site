using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BLL.DTO.Enums;

namespace PL.Models
{
    public class PositionModel
    {
        public int Id { get; set; }
        public int SymbolId { get; set; }
        public SymbolsDTO SymbolType { get; set; }

        [Required]
        [Display(Name = "Symbol")]
        public string SymbolName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yy}", ApplyFormatInEditMode = true)]
        public DateTime OpenDate { get; set; }
        
        [Display(Name = "Open Price")]
        public decimal? OpenPrice { get; set; }

        [Display(Name = "Weight")]
        public int OpenWeight { get; set; }

        [Required]
        [Display(Name = "Trade Type")]
        public TradeTypesDTO TradeType { get; set; }

        [Required]
        [Display(Name = "Trade Status")]
        public TradeStatusesDTO TradeStatus { get; set; }
        
        [Display(Name = "Close Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yy}", ApplyFormatInEditMode = true)]
        public DateTime? CloseDate { get; set; }

        [Display(Name = "Close Price")]
        public decimal? ClosePrice { get; set; }

        [Display(Name = "Current Price")]
        public decimal? CurrentPrice { get; set; }
        public decimal? Gain { get; set; }
        public decimal? AbsoluteGain { get; set; }
        public decimal? MaxGain { get; set; }
    }
}