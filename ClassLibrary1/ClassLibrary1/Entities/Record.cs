using System;
using DAL.Enums;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DAL.Entities
{
    public class Record
    {
        public virtual int Id { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual bool Successfully { get; set; }
        public virtual int EntityId { get; set; }
        public virtual string UserId { get; set; }
        public virtual Enums.Entities Entity { get; set; }
        public virtual Operations Operation { get; set; }
    }

    public class RecordMap : ClassMap<Record>
    {
        public RecordMap()
        {
            Id(x => x.Id);
            Map(x => x.DateTime).Not.Nullable();
            Map(x => x.Successfully).Not.Nullable();
            Map(x => x.EntityId).Not.Nullable();
            Map(x => x.UserId).Not.Nullable();
            Map(x => x.Entity).Not.Nullable();
            Map(x => x.Operation).Not.Nullable();
        }
    }
}
