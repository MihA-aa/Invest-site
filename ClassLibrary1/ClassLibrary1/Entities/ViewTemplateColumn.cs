using DAL.Enums;

namespace DAL.Entities
{
    public class ViewTemplateColumn
    {
        public int Id { get; set; }
        public int DisplayIndex { get; set; }
        public string Name { get; set; }
        public virtual ColumnFormat ColumnFormat { get; set; }
        public int? ColumnFormatId { get; set; }
        public int? FormatId { get; set; }              //I CAN'T DELETE THIS AND I DON'T KNOW WHY -__-
        public virtual Column Column { get; set; }
        public int? ColumnId { get; set; }
        public int? ViewTemplateId { get; set; }
        public virtual ViewTemplate ViewTemplate { get; set; }
    }
}
