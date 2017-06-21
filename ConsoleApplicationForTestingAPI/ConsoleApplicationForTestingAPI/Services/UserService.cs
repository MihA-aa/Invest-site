using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApplicationForTestingAPI.Services
{
    public class UserService
    {
        private HttpClient client;
        private string path;

        public UserService(string path, HttpClient client)
        {
            this.path = path;
            this.client = client;
        }

        public string GetUserInfo(string token)
        {
            var response = client.GetAsync(path + "api/Account/UserInfo").Result;
            return response.Content.ReadAsStringAsync().Result;
        }


        public string Register(string login, string password)
        {
            var registerModel = new
            {
                Login = login,
                Password = password,
                ConfirmPassword = password
            };
            using (var client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(path + "api/Account/Register", registerModel).Result;
                return response.StatusCode.ToString();
            }
        }
    }
}
