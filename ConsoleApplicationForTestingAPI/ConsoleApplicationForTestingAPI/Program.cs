using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplicationForTestingAPI.Models;
using ConsoleApplicationForTestingAPI.Services;
using Newtonsoft.Json;

namespace ConsoleApplicationForTestingAPI
{
    class Program
    {
        private static PortfolioService portfolioService;
        private static PositionService positionService;
        private static UserService userService;
        private static string path = "http://localhost:9101/";
        private static HttpClient client;
        static Program()
        {
            client = CreateClient(GetTokenDictionary("Admin", "Password")["access_token"]);
            portfolioService = new PortfolioService(path, client);
            positionService = new PositionService(path, client);
            userService = new UserService(path, client);
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
            //await positionService.UpdateAllPosition();
            //string userInfo = userService.GetUserInfo(token);
            //Console.WriteLine(userInfo);
            var we = await portfolioService.GetPortfolio(1);
            portfolioService.ShowPortfolio(we);
        }

        static HttpClient CreateClient(string accessToken = "")
        {
            var client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
            return client;
        }

        static Dictionary<string, string> GetTokenDictionary(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password" ),
                    new KeyValuePair<string, string>( "username", userName ),
                    new KeyValuePair<string, string> ( "Password", password )
                };
            var content = new FormUrlEncodedContent(pairs);

            using (var client = new HttpClient())
            {
                var response =
                    client.PostAsync(path + "Token", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                var tokenDictionary = 
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                return tokenDictionary;
            }
        }
    }
}
