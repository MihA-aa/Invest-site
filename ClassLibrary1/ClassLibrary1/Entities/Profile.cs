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
        public virtual string Id { get; set; }
        public virtual string Login { get; set; }
        public virtual User User { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual int? CustomerId { get; set; }
    }
}
