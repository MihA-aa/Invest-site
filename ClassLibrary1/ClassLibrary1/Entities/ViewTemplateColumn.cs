using DAL.Enums;
using DAL.Abstract;

namespace DAL.Entities
{
    public class ViewTemplateColumn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ColumnNames Column { get; set; }
        public virtual Format Format { get; set; }
        public virtual ViewTemplate ViewTemplate { get; set; }
    }
}
