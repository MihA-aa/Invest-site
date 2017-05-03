using DAL.Enums;

namespace DAL.Entities
{
    public class Symbol
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Symbols SymbolType { get; set; }
    }
}
