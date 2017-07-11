using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Portfolio
    {
        public virtual int Id { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual string Name { get; set; }
        public virtual string Notes { get; set; }
        public virtual int DisplayIndex { get; set; }
        public virtual DateTime LastUpdateDate { get; set; }
        public virtual bool Visibility { get; set; }
        public virtual int Quantity { get; set; }
        public virtual decimal PercentWins { get; set; }
        public virtual decimal BiggestWinner { get; set; }
        public virtual decimal BiggestLoser { get; set; }
        public virtual decimal AvgGain { get; set; }
        public virtual decimal MonthAvgGain { get; set; }
        public virtual decimal PortfolioValue { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
        public Portfolio()
        {
            Positions = new List<Position>();
        }
    }
}
