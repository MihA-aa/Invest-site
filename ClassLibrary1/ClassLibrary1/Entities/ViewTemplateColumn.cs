using DAL.Enums;

namespace DAL.Entities
{
    public class ViewTemplateColumn
    {
        public int Id { get; set; }
        public int DisplayIndex { get; set; }
        public string Name { get; set; }
        //public ColumnNames Column { get; set; }
        //public virtual Format Format { get; set; }
        //public int? FormatId { get; set; }
        public virtual Column Column { get; set; }
        public virtual ColumnFormat ColumnFormat { get; set; }
        public int? ViewTemplateId { get; set; }
        public virtual ViewTemplate ViewTemplate { get; set; }
    }
}
