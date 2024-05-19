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

        private void CanExecute_OpenFile(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Execute_OpenFile(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Opening file...");
        }

        private void CanExecute_DeleteText(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(TextBox1.Text);
        }

        private void Execute_DeleteText(object sender, ExecutedRoutedEventArgs e)
        {
            TextBox1.Clear();
        }

        private void CanExecute_CopyText(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(TextBox1.Text);
        }
        private void Execute_CopyText(object sender, ExecutedRoutedEventArgs e)
        {
            Clipboard.SetText(TextBox1.Text);
        }

        private void CanExecute_PasteText(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsText();
        }

        private void Execute_PasteText(object sender, ExecutedRoutedEventArgs e)
        {
            TextBox1.Text += Clipboard.GetText();
        }

        



        public MainWindow()
        {
            InitializeComponent();
        }
    }
}