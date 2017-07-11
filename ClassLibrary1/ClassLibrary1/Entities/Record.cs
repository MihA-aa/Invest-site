using System;
using DAL.Enums;

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
}
