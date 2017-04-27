using ClassLibrary1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Views
{
    class CustomerView1
    {
        public virtual Customer Customer { get; set; }
        public string Name { get; set; }
        public int DisplayIndex { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public int Quantity { get; set; }
        public decimal PercentWins { get; set; }
        public decimal BiggestWinner { get; set; }
        public decimal BiggestLoser { get; set; }
        public decimal AvgGain { get; set; }
        public decimal MonthAvgGain { get; set; }
        public decimal PortfolioValue { get; set; }
    }
}
