using DAL.Enums;

namespace DAL.Entities
{
    public class View
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool ShowName { get; set; }
        public virtual DateFormats DateFormat { get; set; }
        public virtual ViewTemplate ViewTemplate { get; set; }
        public virtual int? ViewTemplateId { get; set; }
        public virtual int MoneyPrecision { get; set; }
        public virtual int PercentyPrecision { get; set; }
        public virtual Portfolio Portfolio { get; set; }
        public virtual int? PortfolioId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
