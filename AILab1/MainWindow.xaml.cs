using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace AILab1
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

        private void BOpen_Click(object sender, RoutedEventArgs e)
        {
            var file = new OpenFileDialog();

            file.Filter = "Graph Data Files (.col)|*.col";
            file.FilterIndex = 1;

            if (file.ShowDialog() == false)
                return;

            var fileName = file.FileName;

            System.IO.StreamReader sr = new System.IO.StreamReader(fileName);

            

            sr.Close();
        }
    }
}
