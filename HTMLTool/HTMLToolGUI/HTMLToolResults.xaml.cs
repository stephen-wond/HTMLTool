using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HTMLToolLogic;

namespace HTMLToolGUI
{
    /// <summary>
    /// Interaction logic for HTMLToolResults.xaml
    /// </summary>
    public partial class HTMLToolResults : Page
    {
        public HTMLToolResults(List<Result> result)
        {
            InitializeComponent();
            DataGrid.ItemsSource = result;
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("Log.txt");
        }
    }
}
