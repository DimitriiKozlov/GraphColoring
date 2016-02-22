using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace AILab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Graph _graph;

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

            var f = new GraphDataFile(new StreamReader(file.FileName), file.SafeFileName);

            LFileName.Content = f.Name;
            LColors.Content = f.NColors;
            LEdges.Content = f.NEdge;
            LVertex.Content = f.NVertex;
            TBFileInfo.Text = f.Comment;

            _graph = new Graph(f);
        }

        private void BClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BAnt_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_graph.Ants(_graph.NVertex / 4).ToString());
        }
    }
}
