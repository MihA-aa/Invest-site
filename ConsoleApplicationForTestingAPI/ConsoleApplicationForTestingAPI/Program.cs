using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplicationForTestingAPI.Models;
using ConsoleApplicationForTestingAPI.Services;

namespace ConsoleApplicationForTestingAPI
{
    class Program
    {
        private static PortfolioService portfolioService;
        private static PositionService positionService;
        static Program()
        {
            portfolioService = new PortfolioService();
            positionService = new PositionService();
        }
        static void Main(string[] args)
        {
            RunAsync();
            Console.ReadLine();
        }

        static async Task RunAsync()
        {
            //var w = await portfolioService.GetAllPortfolio();
            //portfolioService.ShowPortfolios(w.ToList());
            //await portfolioService.CreatePortfolio(new PortfolioModel {Id = 1024, Name = "New Portfolio", DisplayIndex = 7, Notes = "Some Notes", Visibility = true});
            //await portfolioService.UpdatePortfolio(new PortfolioModel { Id = 1007, Name = "Update234 Portfolio", DisplayIndex = 7, Notes = "Some Notes", Visibility = true }); 
            //var w = await portfolioService.GetPortfolioPosition(1);
            //positionService.ShowPositions(w.ToList());


        }
    }
}
