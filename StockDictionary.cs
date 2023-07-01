using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace RealWorldIntFinal
{
	public class StockDictionary
	{
		Dictionary<string, decimal> stocks = new Dictionary<string, decimal>();
		private XmlDocument userStockDoc = new XmlDocument();

		/// <summary>
		/// Constructor
		/// </summary>
		public StockDictionary()
		{
		}

		/// <summary>
		/// Methods for add or update existing stocks list
		/// </summary>
		/// <param name="symbol"></param>
		/// <param name="price"></param>
		public void addOrUpdateStock(string symbol, decimal price, string userName) {
			try
			{
				if (!stocks.ContainsKey(symbol))
				{
					stocks.Add(symbol, price);
				}
				else
				{
					// Updating the price of symbol with new price
					stocks[symbol] = price;
				}
				saveToXml($"{userName}_stocks.xml", stocks);
			}
			catch
			{
				throw new Exception("Stock addition/update failed.");
			}

		}

		/// <summary>
		/// Delete stock from list
		/// </summary>
		/// <param name="symbol"></param>
		public void removeStock(string symbol) {
			try
			{
				if (stocks.ContainsKey(symbol))
				{
					stocks.Remove(symbol);
				}
			}
			catch
			{
				throw new Exception("stock deletion failed.");
			}
		}

		/// <summary>
		/// Save stock details to XML file
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="stock"></param>
		public void saveToXml(string filePath, Dictionary<string, decimal> stock) {
			if(!File.Exists(filePath))
			{
				XElement root = new XElement("root", from keyValue in stock 
													 select new XElement(keyValue.Key, keyValue.Value));
				root.Save(filePath);
			}
			else
			{
				File.Delete(filePath);
                XElement root = new XElement("root", from keyValue in stock
                                                     select new XElement(keyValue.Key, keyValue.Value));
                root.Save(filePath);
            }
		}


		/// <summary>
		/// Load stock details from xml file
		/// </summary>
		/// <param name="filePath"></param>
		public List<Stock> loadFromXml(string filePath) {
			List<Stock> userStocks = new List<Stock>();
			if(File.Exists(filePath))
			{
				userStockDoc.Load(filePath);
				XmlNodeList dataNodes = userStockDoc.SelectNodes("//root");
				if (dataNodes != null) {
					foreach (XmlNode node in dataNodes)
					{
						foreach (XmlNode childNode in node.ChildNodes)
						{
							userStocks.Add(new Stock(childNode.LocalName, decimal.Parse(childNode.InnerText)));
						}
					}
				}
			}
			return userStocks;
		}
	}
}

