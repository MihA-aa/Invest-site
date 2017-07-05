using System;
using DAL.Enums;

namespace DAL.Entities
{
    public class Record
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public bool Successfully { get; set; }
        public int EntityId { get; set; }
        public string UserId { get; set; }
        public Enums.Entities Entity { get; set; }
        public Operations Operation { get; set; }
    }
}
