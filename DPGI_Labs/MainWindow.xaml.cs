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
            // Приклад даних, додайте більше курсів за потреби
            exchangeRates = new Dictionary<string, CurrencyInfo>
            {
                { "USD", new CurrencyInfo { Name = "United States Dollar", Rate = 27.50 } },
                { "EUR", new CurrencyInfo { Name = "Euro", Rate = 32.00 } },
                { "GBP", new CurrencyInfo { Name = "British Pound Sterling", Rate = 37.50 } },
                { "JPY", new CurrencyInfo { Name = "Japanese Yen", Rate = 0.25 } },
                { "AUD", new CurrencyInfo { Name = "Australian Dollar", Rate = 20.00 } },
                { "CAD", new CurrencyInfo { Name = "Canadian Dollar", Rate = 21.00 } },
                { "CHF", new CurrencyInfo { Name = "Swiss Franc", Rate = 29.00 } },
                { "CNY", new CurrencyInfo { Name = "Chinese Yuan", Rate = 4.30 } },
                { "SEK", new CurrencyInfo { Name = "Swedish Krona", Rate = 3.10 } },
                { "NOK", new CurrencyInfo { Name = "Norwegian Krone", Rate = 3.15 } }
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