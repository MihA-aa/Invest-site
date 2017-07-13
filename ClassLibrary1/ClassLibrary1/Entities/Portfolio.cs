using System;
using System.Collections.Generic;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DAL.Entities
{
    public class Portfolio
    {
        public virtual int Id { get; set; }
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
        public virtual Customer Customer { get; set; }

        private IList<Position> _positions;
        public virtual IList<Position> Positions
        {
            get
            {
                return _positions ?? (_positions = new List<Position>());
            }
            set { _positions = value; }
        }
    }

    public class PortfolioMap : ClassMapping<Portfolio>
    {
        public PortfolioMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.Name);
            Property(x => x.Notes);
            Property(x => x.DisplayIndex);
            Property(x => x.LastUpdateDate);
            Property(x => x.Visibility);
            Property(x => x.Quantity);
            Property(x => x.PercentWins);
            Property(x => x.BiggestWinner);
            Property(x => x.BiggestLoser);
            Property(x => x.AvgGain);
            Property(x => x.MonthAvgGain);
            Property(x => x.PortfolioValue);
            ManyToOne(x => x.Customer,
            c =>
            {
                c.Cascade(Cascade.Persist);
                c.Column("Customer_Id");
            });
            Bag(x => x.Positions,
            c => { c.Key(k => k.Column("Portfolio_Id")); c.Inverse(true); },
            r => r.OneToMany());
        }
    }
}
