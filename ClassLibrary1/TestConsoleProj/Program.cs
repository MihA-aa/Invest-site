using ClassLibrary1.Entities;
using ClassLibrary2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleProj
{
    class Program
    {
        static void Main(string[] args)
        {
            EFUnitOfWork db = new EFUnitOfWork();
            var portfolios = db.Portfolios.GetAll().ToList();
            var positions = db.Positions.GetAll().ToList();
            var symbols = db.Symbols.GetAll().ToList();

            foreach (Portfolio u in portfolios)
                Console.WriteLine("{0} - {1} - {2} - {3} - {4} - {5}", u.Name, u.DisplayIndex, u.Quantity, u.PercentWins, u.BiggestWinner, u.BiggestLoser);
            Console.WriteLine();

            foreach (Position u in positions)
                Console.WriteLine("{0} - {1} - {2} - {3} - {4} - {5} - {6}", u.Name, u.SymbolName, u.SymbolType, u.OpenDate, u.OpenPrice, u.CloseDate, u.ClosePrice);
            Console.WriteLine();

            foreach (Symbol u in symbols)
                Console.WriteLine("{0} - {1}", u.Name, u.SymbolType);
            Console.WriteLine();

            Console.Read();
        }
    }
}
