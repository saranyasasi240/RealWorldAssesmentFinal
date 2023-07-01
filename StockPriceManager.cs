using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RealWorldIntFinal
{
    public class StockPriceManager
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Constructor
        /// </summary>
        public StockPriceManager()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// To get the price of a stock
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public async Task<decimal> getPriceAsync(string symbol, string apiKey)
        {
            try
            {
                string jsonResponse = await fetchStockPrice(symbol, apiKey);
                StockPrice stockPrice = JsonSerializer.Deserialize<StockPrice>(jsonResponse);
                decimal price = decimal.Parse(stockPrice.Price);
                return price;
            }
            catch
            {
                throw new JsonException("Error in getPriceAsync method");
            }
        }

        /// <summary>
        /// To fetch stock price
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="apiKey"></param>
        /// <returns>stock price</returns>
        /// <exception cref="JsonException"></exception>
        private async Task<string> fetchStockPrice(string symbol, string apiKey)
        {
            string url = $"https://api.twelvedata.com/price?symbol={symbol}&apikey={apiKey}";
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);

            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                return jsonResponse;
            }
            else 
            { 
                throw new JsonException("Error in fetch stock price."); 
            }
        }
    }
}
