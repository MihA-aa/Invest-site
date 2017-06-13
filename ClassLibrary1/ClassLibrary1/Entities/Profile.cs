using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Profile
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public string Login { get; set; }
        public virtual User User { get; set; }
        public virtual Customer Customer { get; set; }
        public int? CustomerId { get; set; }
    }
}
