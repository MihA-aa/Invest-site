using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;
using NHibernate.Mapping;
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
        
        public virtual IList<Position> Positions { get; set; }
    }

    public class PortfolioMap : ClassMap<Portfolio>
    {
        public PortfolioMap()
        {
            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable().Length(200);
            Map(x => x.Notes).Length(200);
            Map(x => x.DisplayIndex).Not.Nullable();
            Map(x => x.LastUpdateDate).Not.Nullable();
            Map(x => x.Visibility).Not.Nullable();
            Map(x => x.Quantity).Not.Nullable();
            Map(x => x.PercentWins).Not.Nullable();
            Map(x => x.BiggestWinner).Not.Nullable();
            Map(x => x.BiggestLoser).Not.Nullable();
            Map(x => x.AvgGain).Not.Nullable();
            Map(x => x.MonthAvgGain).Not.Nullable();
            Map(x => x.PortfolioValue).Not.Nullable();
            References(x => x.Customer).Column("Customer").Not.Nullable();
            HasMany(x => x.Positions).Inverse().Cascade.All().KeyColumn("Portfolio");
        }
    }
}
