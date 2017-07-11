using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<Portfolio> Portfolios { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }
        public virtual ICollection<View> Views { get; set; }
        public virtual ICollection<ViewTemplate> ViewTemplates { get; set; }
        public Customer()
        {
            Portfolios = new List<Portfolio>();
            Profiles = new List<Profile>();
            Views = new List<View>();
            ViewTemplates = new List<ViewTemplate>();
        }
    }
}
