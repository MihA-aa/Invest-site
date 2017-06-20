using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace ConsoleApplicationForTestingAPI.Models
{
    public class PortfolioModel
    {
        public int Id { get; set; }
        public int DisplayIndex { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Visibility { get; set; }
    }
}
