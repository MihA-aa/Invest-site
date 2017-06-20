using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using ConsoleApplicationForTestingAPI.Models;


namespace ConsoleApplicationForTestingAPI.Services
{
    public class PortfolioService
    {
        private HttpClient client;
        public PortfolioService()
        {
            client = new HttpClient();
        }
        public async Task<IEnumerable<PortfolioModel>> GetAllPortfolio()
        {
            IEnumerable<PortfolioModel> portfolios = null;
            var result = await client.GetAsync("http://localhost:9101/api/portfolio");
            if (result.IsSuccessStatusCode)
            {
                portfolios = await result.Content.ReadAsAsync<IEnumerable<PortfolioModel>>();
            }
            else
            {
                Console.WriteLine(result.Content.ReadAsStringAsync().Result);
            }
            return portfolios;
        }
        public async Task<IEnumerable<PortfolioInformationModel>> GetAllPortfolioInformation()
        {
            IEnumerable<PortfolioInformationModel> portfolios = null;
            var result = await client.GetAsync("http://localhost:9101/api/PortfolioInformation");
            if (result.IsSuccessStatusCode)
            {
                portfolios =  await result.Content.ReadAsAsync<IEnumerable<PortfolioInformationModel>>();
            }
            else
            {
                Console.WriteLine(result.Content.ReadAsStringAsync().Result);
            }
            return portfolios;
        }

        public async Task<PortfolioModel> GetPortfolio(int portfolioId)
        {
            PortfolioModel portfolio = null;
            var result = await client.GetAsync("http://localhost:9101/api/Portfolio/" + portfolioId);
            if (result.IsSuccessStatusCode)
            {
                portfolio = await result.Content.ReadAsAsync<PortfolioModel>();
            }
            else
            {
                Console.WriteLine(result.Content.ReadAsStringAsync().Result);
            }
            return portfolio;
        }

        public async Task<IEnumerable<PositionModel>> GetPortfolioPosition(int portfolioId)
        {
            IEnumerable <PositionModel> positions = null;
            var result = await client.GetAsync("http://localhost:9101/api/portfolio/" + portfolioId + "/positions");
            if (result.IsSuccessStatusCode)
            {
                positions = await result.Content.ReadAsAsync<IEnumerable<PositionModel>>();
                Console.WriteLine(positions);
            }
            else
            {
                Console.WriteLine(result.Content.ReadAsStringAsync().Result);
            }
            return positions;
        }

        public async Task<PortfolioInformationModel> GetPortfolioInformation(int portfolioId)
        {
            PortfolioInformationModel portfolio = null;
            var result = await client.GetAsync("http://localhost:9101/api/PortfolioInformation/" + portfolioId);
            if (result.IsSuccessStatusCode)
            {
                portfolio = await result.Content.ReadAsAsync<PortfolioInformationModel>();
            }
            else
            {
                Console.WriteLine(result.Content.ReadAsStringAsync().Result);
            }
            return portfolio;
        }

        public async Task CreatePortfolio(PortfolioModel portfolio)
        {
            var result = await client.PostAsJsonAsync("http://localhost:9101/api/Portfolio", portfolio);
            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Portfolio creation was unsuccessful\n" + result.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine("Portfolio creation was successful");
            }
        }

        public async Task UpdatePortfolio(PortfolioModel portfolio)
        {
            var result = await client.PutAsJsonAsync("http://localhost:9101/api/Portfolio", portfolio);
            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Portfolio update was unsuccessful\n" + result.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine("Portfolio update was successful");
            }
        }

        public async Task DeletePortfolio(int portfolioId)
        {
            PortfolioModel portfolio = null;
            var result = await client.DeleteAsync("http://localhost:9101/api/Portfolio/" + portfolioId);
            if (result.IsSuccessStatusCode)
            {
                Console.WriteLine("Portfolio delte was successful");
            }
            else
            {
                Console.WriteLine(result.Content.ReadAsStringAsync().Result);
            }
        }

        public void ShowPortfolioInformation(PortfolioInformationModel portfolio)
        {
            Console.WriteLine("Name:\t\t\t{0}\nLast Update Date:\t{1}\nQuantity of positions:\t{2}\nPercent of wins:\t{3}\n" +
                              "Biggest winner:\t\t{4}\nBiggest loser:\t\t{5}\nAverage Gain:\t\t{6}\nMonth Average Gain:\t{7}\nPortfolio Value:\t{8}\n",
                portfolio.Name, portfolio.LastUpdateDate, portfolio.Quantity, portfolio.PercentWins, portfolio.BiggestWinner,
                portfolio.BiggestLoser, portfolio.AvgGain, portfolio.MonthAvgGain, portfolio.PortfolioValue);
        }

        public void ShowPortfolio(PortfolioModel portfolio)
        {
            Console.WriteLine("Id:\t\t{0}\nName:\t\t{1}\nDisplayIndex:\t{2}\nVisibility:\t{3}\n" +"Notes:\t\t{4}\n",
                portfolio.Id, portfolio.Name, portfolio.DisplayIndex, portfolio.Visibility, portfolio.Notes);
        }

        public void ShowPortfolios(List<PortfolioModel> portfolios)
        {
            foreach (var portfolio in portfolios)
            {
                Console.WriteLine("Id:\t\t{0}\nName:\t\t{1}\nDisplayIndex:\t{2}\nVisibility:\t{3}\n" + "Notes:\t\t{4}\n",
                    portfolio.Id, portfolio.Name, portfolio.DisplayIndex, portfolio.Visibility, portfolio.Notes);
            }
        }

        public void ShowPortfoliosInFormation(List<PortfolioInformationModel> portfolios)
        {
            foreach (var portfolio in portfolios)
            {
                Console.WriteLine("Name:\t\t\t{0}\nLast Update Date:\t{1}\nQuantity of positions:\t{2}\nPercent of wins:\t{3}\n" +
                                  "Biggest winner:\t\t{4}\nBiggest loser:\t\t{5}\nAverage Gain:\t\t{6}\nMonth Average Gain:\t{7}\nPortfolio Value:\t{8}\n",
                    portfolio.Name, portfolio.LastUpdateDate, portfolio.Quantity, portfolio.PercentWins, portfolio.BiggestWinner,
                    portfolio.BiggestLoser, portfolio.AvgGain, portfolio.MonthAvgGain, portfolio.PortfolioValue);
            }
        }

    }
}
