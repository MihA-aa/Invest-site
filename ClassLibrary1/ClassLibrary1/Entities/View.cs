using DAL.Enums;

namespace DAL.Entities
{
    public class View
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ShowName { get; set; }
        public DateFormats DateFormat { get; set; }
        public virtual ViewTemplate ViewTemplate { get; set; }
        public int MoneyPrecision { get; set; }
        public int PercentyPrecision { get; set; }
        public virtual Portfolio Portfolio { get; set; }
    }
}
