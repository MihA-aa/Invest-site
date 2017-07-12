using System;
using DAL.Enums;
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

    public class RecordMap : ClassMapping<Record>
    {
        public RecordMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.DateTime);
            Property(x => x.Successfully);
            Property(x => x.EntityId);
            Property(x => x.UserId);
            Property(x => x.Entity);
            Property(x => x.Operation);
        }
    }
}
