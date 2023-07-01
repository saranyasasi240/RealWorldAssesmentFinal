using System;
namespace RealWorldIntFinal
{
	public class User
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public StockDictionary UserStocks { get; set; }
		public string UserStockFile { get; set; }

		/// <summary>
		/// Constructor method
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		public User(string username,string password)
		{
			Username = username;
			Password = password;
			UserStockFile = $"{username}_stocks.xml";
			//UserStocks=

		}
	}
}

