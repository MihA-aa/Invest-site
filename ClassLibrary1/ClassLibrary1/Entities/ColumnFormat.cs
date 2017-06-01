using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ColumnFormat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Format> Formats { get; set; }
        public ColumnFormat()
        {
            Formats = new List<Format>();
        }
    }
}
