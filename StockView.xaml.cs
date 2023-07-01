using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace RealWorldIntFinal
{
    /// <summary>
    /// Interaction logic for StockView.xaml
    /// </summary>
    public partial class StockView : Window
    {
        public User UserData { get; set; }

        StockDictionary stockDictionary = new StockDictionary();
        public StockView(User userData)
        {
            InitializeComponent();
            this.UserData = userData;
            gbxAddStock.Visibility = Visibility.Hidden;
            StockView1.Visibility = Visibility.Visible;
            loadUserStocks();
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            StockPriceManager stockPriceManager = new StockPriceManager();
            int flag = 0;

            XmlDocument symbolDocs = new XmlDocument();
            symbolDocs.Load(Constants.StockFile);
            XmlNodeList dataNodes = symbolDocs.SelectNodes("//Symbols");
            foreach (XmlNode node in dataNodes)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (txtSymbolSearch.Text == childNode.InnerText)
                    {
                        flag = 1;
                        gbxAddStock.Visibility = Visibility.Visible;
                        lblStockSymbols.Content = childNode.InnerText;
                        lblStockPrice.Content = await stockPriceManager.getPriceAsync(childNode.InnerText, Constants.ApiKey);
                        break;
                    }
                }
            }
            if (flag == 0)
            {
                MessageBox.Show("Item not found.");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            stockDictionary.addOrUpdateStock(lblStockSymbols.Content.ToString(), decimal.Parse(lblStockPrice.Content.ToString()), UserData.Username);
            loadUserStocks();
        }

        public void loadUserStocks()
        {
            gridStock.Items.Add(stockDictionary.loadFromXml(UserData.UserStockFile));
        }
    }
}
