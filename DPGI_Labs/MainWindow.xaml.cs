using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DPGI_Labs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public class CurrencyInfo
    {
        public string Name { get; set; }
        public double Rate { get; set; }
    }
    public partial class MainWindow : Window
    {
        private Dictionary<string, CurrencyInfo> exchangeRates;
        public MainWindow()
        {
            InitializeComponent(); 
            LoadExchangeRates();
            InitializeCurrencyComboBox();
        }

        private void LoadExchangeRates()
        {
            exchangeRates = new Dictionary<string, CurrencyInfo>
            {
                { "USD", new CurrencyInfo { Name = "United States Dollar", Rate = 39.83 } },
                { "EUR", new CurrencyInfo { Name = "Euro", Rate = 43.20 } },
                { "GBP", new CurrencyInfo { Name = "British Pound Sterling", Rate = 50.69 } },
                { "JPY", new CurrencyInfo { Name = "Japanese Yen", Rate = 0.25 } },
                { "AUD", new CurrencyInfo { Name = "Australian Dollar", Rate = 26.43 } },
                { "CAD", new CurrencyInfo { Name = "Canadian Dollar", Rate = 29.13 } },
                { "CHF", new CurrencyInfo { Name = "Swiss Franc", Rate = 43.61 } },
                { "CNY", new CurrencyInfo { Name = "Chinese Yuan", Rate = 5.49 } },
                { "SEK", new CurrencyInfo { Name = "Swedish Krona", Rate = 3.74 } },
                { "NOK", new CurrencyInfo { Name = "Norwegian Krone", Rate = 3.75 } }
            };
        }

        private void InitializeCurrencyComboBox()
        {
            foreach (var currency in exchangeRates)
            {
                CurrencyComboBox.Items.Add(new { Key = currency.Key, Name = $"{currency.Key} - {currency.Value.Name}" });
            }
            CurrencyComboBox.SelectedIndex = 0;
        }

        private void ConvertToUahButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double amount = double.Parse(AmountTextBox.Text);
                string selectedCurrency = ((dynamic)CurrencyComboBox.SelectedItem).Key;
                double rate = exchangeRates[selectedCurrency].Rate;
                double result = amount * rate;
                ResultTextBlock.Text = $"{amount} {selectedCurrency} = {result} грн";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Будь ласка, введіть коректні дані.");
            }
        }

        private void ConvertFromUahButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double amount = double.Parse(AmountTextBox.Text);
                string selectedCurrency = ((dynamic)CurrencyComboBox.SelectedItem).Key;
                double rate = exchangeRates[selectedCurrency].Rate;
                double result = amount / rate;
                ResultTextBlock.Text = $"{amount} грн = {result} {selectedCurrency}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Будь ласка, введіть коректні дані.");
            }
        }
    }
}