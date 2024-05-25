using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;
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

    public class CurrencyContext : DbContext 
    {
        public DbSet<CurrencyInfo> ExchangeRates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-IO5GGS0;Database=CurrencyConverterDB;Trusted_Connection=True;");
        }
    }

    public class CurrencyInfo
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public double Rate { get; set; }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadExchangeRates();
            //InitializeCurrencyComboBox();
        }

        private void LoadExchangeRates()
        {
           var exchangeRates = new Dictionary<string, CurrencyInfo>();

            using (var context = new CurrencyContext())
            {
                exchangeRates = context.ExchangeRates.ToDictionary(rate => rate.CurrencyCode, rate => new CurrencyInfo
                {
                    CurrencyCode = rate.CurrencyCode,
                    CurrencyName = rate.CurrencyName,
                    Rate = rate.Rate
                });
            }

            var exchangeRatesList = exchangeRates.Select(kvp => new { Key = kvp.Key, Name = $"{kvp.Key} - {kvp.Value.CurrencyName}" }).ToList();

            CurrencyComboBox.ItemsSource = exchangeRatesList;
            CurrencyComboBox.DisplayMemberPath = "Name";
            CurrencyComboBox.SelectedValuePath = "Key";
            CurrencyComboBox.SelectedIndex = 0;

            foreach (var currency in exchangeRates)
            {
                CurrencyComboBox.Items.Add(new { Key = currency.Key, Name = $"{currency.Key} - {currency.Value.CurrencyName}" });
            }
            CurrencyComboBox.SelectedIndex = 0;
        }

        //private void InitializeCurrencyComboBox()
        //{
        //    foreach (var currency in exchangeRates)
        //    {
        //        CurrencyComboBox.Items.Add(new { Key = currency.Key, Name = $"{currency.Key} - {currency.Value.Name}" });
        //    }
        //    CurrencyComboBox.SelectedIndex = 0;
        //}

        private void ConvertToUahButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double amount = double.Parse(AmountTextBox.Text);
                string selectedCurrency = ((dynamic)CurrencyComboBox.SelectedItem).Key;

                using (var context = new CurrencyContext())
                {
                    var rate = context.ExchangeRates.Single(r => r.CurrencyCode == selectedCurrency).Rate;
                    double result = amount * rate;
                    ResultTextBlock.Text = $"{amount} {selectedCurrency} = {result} грн";
                }
            }
            catch (Exception)
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

                using (var context = new CurrencyContext())
                {
                    var rate = context.ExchangeRates.Single(r => r.CurrencyCode == selectedCurrency).Rate;
                    double result = amount / rate;
                    ResultTextBlock.Text = $"{amount} грн = {result} {selectedCurrency}";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Будь ласка, введіть коректні дані.");
            }
        }

        private void InitializeDatabase()
        {
            using (var context = new CurrencyContext())
            {
                context.Database.EnsureCreated();

                if (!context.ExchangeRates.Any())
                {
                    context.ExchangeRates.AddRange(new List<CurrencyInfo>
                    {
                        new CurrencyInfo { CurrencyCode = "USD", CurrencyName = "United States Dollar", Rate = 27.50 },
                        new CurrencyInfo { CurrencyCode = "EUR", CurrencyName = "Euro", Rate = 32.00 },
                        new CurrencyInfo { CurrencyCode = "GBP", CurrencyName = "British Pound Sterling", Rate = 37.50 },
                        new CurrencyInfo { CurrencyCode = "JPY", CurrencyName = "Japanese Yen", Rate = 0.25 },
                        new CurrencyInfo { CurrencyCode = "AUD", CurrencyName = "Australian Dollar", Rate = 20.00 },
                        new CurrencyInfo { CurrencyCode = "CAD", CurrencyName = "Canadian Dollar", Rate = 21.00 },
                        new CurrencyInfo { CurrencyCode = "CHF", CurrencyName = "Swiss Franc", Rate = 29.00 },
                        new CurrencyInfo { CurrencyCode = "CNY", CurrencyName = "Chinese Yuan", Rate = 4.30 },
                        new CurrencyInfo { CurrencyCode = "SEK", CurrencyName = "Swedish Krona", Rate = 3.10 },
                        new CurrencyInfo { CurrencyCode = "NOK", CurrencyName = "Norwegian Krone", Rate = 3.15 }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}