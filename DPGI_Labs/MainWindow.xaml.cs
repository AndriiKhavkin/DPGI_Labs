using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            //var connectionString = "Data Source=DESKTOP-IO5GGS0;Initial Catalog=DPGI_DB;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True" />
            var connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            Console.Write(connectionString);
            DataAccess students = new DataAccess(connectionString);
            list.SelectedIndex = 0;
            list.Focus();
            list.DataContext = students.GetDataFromDatabase();
            base.OnInitialized(e);
        }

        private void createItem(object sender, RoutedEventArgs e)
        {
            string id = IdTextBox.Text;
            string studentID = StudentIDTextBox.Text;
            string fullName = FullNameTextBox.Text;
            string group = GroupTextBox.Text;
            string address = AddressTextBox.Text;

            var connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            Console.Write(connectionString);
            DataAccess students = new DataAccess(connectionString);
            var myQuery = $"INSERT INTO Products (StudentID, FullName, Group, Address) VALUES ('{studentID}', '{fullName}', '{group}', '{address}');";
            students.InsertRecord(myQuery);
            list.DataContext = students.GetDataFromDatabase();
        }

        private void udpateItem(object sender, RoutedEventArgs e)
        {
            string id = IdTextBox.Text;
            string studentID = StudentIDTextBox.Text;
            string fullName = FullNameTextBox.Text;
            string group = GroupTextBox.Text;
            string address = AddressTextBox.Text;

            var connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            Console.Write(connectionString);
            DataAccess students = new DataAccess(connectionString);
            var myQuery = $"UPDATE Products SET StudentID = '{studentID}', FullName = '{fullName}', Group = '{group}', Address = '{address}' WHERE Id = '{id}';";
            students.InsertRecord(myQuery);
            list.DataContext = students.GetDataFromDatabase();
        }

        private void deleteItem(object sender, RoutedEventArgs e)
        {
            string id = IdTextBox.Text;
            var connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            Console.Write(connectionString);
            DataAccess students = new DataAccess(connectionString);
            var myQuery = $"DELETE FROM Products WHERE Id = {id}; ";
            students.InsertRecord(myQuery);
            list.DataContext = students.GetDataFromDatabase();
        }


    }
}