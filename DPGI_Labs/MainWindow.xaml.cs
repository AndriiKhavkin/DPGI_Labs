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
    public partial class MainWindow : Window
    {
        private Dictionary<string, double> exchangeRates;
        public MainWindow()
        {
            InitializeComponent(); 
            LoadExchangeRates();
            InitializeCurrencyComboBox();
        }

        private void LoadExchangeRates()
        {
            // Приклад даних, додайте більше курсів за потреби
            exchangeRates = new Dictionary<string, double>
            {
                { "USD", 27.50 },
                { "EUR", 32.00 },
                { "GBP", 37.50 }
            };
        }

        private void InitializeCurrencyComboBox()
        {
            foreach (var currency in exchangeRates.Keys)
            {
                CurrencyComboBox.Items.Add(currency);
            }
            CurrencyComboBox.SelectedIndex = 0;
        }

        private void ConvertToUahButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double amount = double.Parse(AmountTextBox.Text);
                string selectedCurrency = CurrencyComboBox.SelectedItem.ToString();
                double rate = exchangeRates[selectedCurrency];
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
                string selectedCurrency = CurrencyComboBox.SelectedItem.ToString();
                double rate = exchangeRates[selectedCurrency];
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