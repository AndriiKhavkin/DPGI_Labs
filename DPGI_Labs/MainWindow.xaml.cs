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
        private void CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            if (TextBox1.Text.Trim().Length > 0)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void Execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            System.IO.File.WriteAllText($"{startupPath}\\texts\\123.txt", TextBox1.Text);
            MessageBox.Show("The file was saved!");

        }


        public MainWindow()
        {
            InitializeComponent();
        }
    }
}