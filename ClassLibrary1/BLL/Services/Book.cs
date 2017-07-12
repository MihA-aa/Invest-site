using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace BLL.Services
{
    public class Book
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }

    public class BookMap : ClassMapping<Book>
    {
        public BookMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.Name);
            Property(x => x.Description);
        }
    }
}
