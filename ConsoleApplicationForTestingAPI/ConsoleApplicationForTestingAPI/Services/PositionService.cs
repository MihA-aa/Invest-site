using System;
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
        public PositionService()
        {
            client = new HttpClient();
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
