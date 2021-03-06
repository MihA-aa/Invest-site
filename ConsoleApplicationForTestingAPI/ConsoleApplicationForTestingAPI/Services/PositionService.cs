﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplicationForTestingAPI.Models;

namespace ConsoleApplicationForTestingAPI.Services
{
    public class PositionService
    {
        private HttpClient client;
        private string path;

        public PositionService(string path, HttpClient client)
        {
            this.path = path;
            this.client = client;
        }

        public async Task UpdatePosition(int? id)
        {
            var result = await client.PutAsJsonAsync(path + "api/Position/Update", id);
            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Position update was unsuccessful\n" + result.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine("Position update was successful");
            }
        }

        public async Task UpdateAllPosition()
        {
            var result = await client.PutAsJsonAsync(path + "api/position/AllUpdate", "");
            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("All positions updated unsuccessfully\n" + result.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine("All positions updated successfully");
            }
        }

        public void ShowPosition(PositionModel position)
        {
            Console.WriteLine("Id:\t\t{0}\nName:\t\t{1}\nSymbolId:\t{2}\nOpenDate:\t{3}\nOpenPrice:\t{4}\nOpenWeight:\t{5}\n" +
                              "TradeType:\t{6}\nTradeStatus:\t{7}\nCloseDate:\t{8}\nClosePrice:\t{9}\nCurrentPrice:\t{10}\n" +
                              "LastUpdateDate:\t{11}\nLastUpdatePrice:{12}\nGain:\t\t{13}\nAbsoluteGain:\t{14}\nMaxGain:\t{15}\n" +
                              "Dividends:\t{16}\nCurrencySymbol:\t{17}\n",
                position.Id, position.Name, position.SymbolId, position.OpenDate, position.OpenPrice, position.OpenWeight, position.TradeType,
                position.TradeStatus, position.CloseDate, position.ClosePrice, position.CurrentPrice, position.LastUpdateDate, position.LastUpdatePrice,
                position.Gain, position.AbsoluteGain, position.MaxGain, position.Dividends, position.CurrencySymbol );
        }

        public void ShowPositions(List<PositionModel> positions)
        {
            foreach (var position in positions)
            {
                Console.WriteLine("Id:\t\t{0}\nName:\t\t{1}\nSymbolId:\t{2}\nOpenDate:\t{3}\nOpenPrice:\t{4}\nOpenWeight:\t{5}\n" +
                                  "TradeType:\t{6}\nTradeStatus:\t{7}\nCloseDate:\t{8}\nClosePrice:\t{9}\nCurrentPrice:\t{10}\n" +
                                  "LastUpdateDate:\t{11}\nLastUpdatePrice:{12}\nGain:\t\t{13}\nAbsoluteGain:\t{14}\nMaxGain:\t{15}\n" +
                                  "Dividends:\t{16}\nCurrencySymbol:\t{17}\n",
                    position.Id, position.Name, position.SymbolId, position.OpenDate, position.OpenPrice, position.OpenWeight, position.TradeType,
                    position.TradeStatus, position.CloseDate, position.ClosePrice, position.CurrentPrice, position.LastUpdateDate, position.LastUpdatePrice,
                    position.Gain, position.AbsoluteGain, position.MaxGain, position.Dividends, position.CurrencySymbol);
            }
        }
    }
}
