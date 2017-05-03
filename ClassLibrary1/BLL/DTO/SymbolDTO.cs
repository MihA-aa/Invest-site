using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Enums;

namespace BLL.DTO
{
    public class SymbolDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Symbols SymbolType { get; set; }
    }
}
