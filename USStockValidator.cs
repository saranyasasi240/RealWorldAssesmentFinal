using System.Collections.Generic;

namespace RealWorldIntFinal
{
	public class USStockValidator
	{
		Dictionary<string, decimal> validStocks = new Dictionary<string, decimal>();

		public USStockValidator()
		{
		}

		/// <summary>
		/// To check the selected stock is valid or not
		/// </summary>
		/// <param name="symbol"></param>
		/// <returns></returns>
		public bool validateStockSymbol(string symbol) {
			return true;
		}
	}
}

