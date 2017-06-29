using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Enums;

namespace BLL.DTO
{
    public class RecordDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Successfully { get; set; }
        public DateTime DateTime { get; set; }
        public int EntityId { get; set; }
        public string UserId { get; set; }
        public EntitiesDTO Entity { get; set; }
        public OperationsDTO Operation { get; set; }
    }
}
