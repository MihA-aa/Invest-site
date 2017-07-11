using DAL.Enums;

namespace DAL.Entities
{
    public class ViewTemplateColumn
    {
        public virtual int Id { get; set; }
        public virtual int DisplayIndex { get; set; }
        public virtual string Name { get; set; }
        public virtual ColumnFormat ColumnFormat { get; set; }
        public virtual int? ColumnFormatId { get; set; }
        public virtual int? FormatId { get; set; }              //I CAN'T DELETE THIS AND I DON'T KNOW WHY -__-
        public virtual Column Column { get; set; }
        public virtual int? ColumnId { get; set; }
        public virtual int? ViewTemplateId { get; set; }
        public virtual ViewTemplate ViewTemplate { get; set; }
    }
}
