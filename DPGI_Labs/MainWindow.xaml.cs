using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;
using System.Text;
using System.Windows;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
        public int Id { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public double Rate { get; set; }
    }
    public partial class MainWindow : Window
    {
        private Dictionary<string, CurrencyInfo> exchangeRates;
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            DataAccess CurrencyDB = new DataAccess(connectionString);
            List<CurrencyInfo> currencies = CurrencyDB.GetDataFromDatabase();

            exchangeRates = currencies.ToDictionary(c => c.CurrencyCode, c => c);

            InitializeCurrencyComboBox();
            base.OnInitialized(e);
        }

        private void InitializeCurrencyComboBox()
        {
            var exchangeRatesList = exchangeRates.Select(kvp => new { Key = kvp.Key, Name = $"{kvp.Key} - {kvp.Value.CurrencyName}" }).ToList();
            CurrencyComboBox.ItemsSource = exchangeRatesList;
            CurrencyComboBox.DisplayMemberPath = "Name";
            CurrencyComboBox.SelectedValuePath = "Key";
            CurrencyComboBox.SelectedIndex = 0;
        }


        private void ConvertToUahButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double amount = double.Parse(AmountTextBox.Text);
                string selectedCurrency = ((dynamic)CurrencyComboBox.SelectedItem).Key;

                if (exchangeRates.TryGetValue(selectedCurrency, out CurrencyInfo currencyInfo))
                {
                    double result = amount * currencyInfo.Rate;
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

                if (exchangeRates.TryGetValue(selectedCurrency, out CurrencyInfo currencyInfo))
                {
                    double result = amount / currencyInfo.Rate;
                    ResultTextBlock.Text = $"{amount} грн = {result} {selectedCurrency}";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Будь ласка, введіть коректні дані.");
            }
        }             
    }
}