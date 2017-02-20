using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for HTMLToolHome.xaml
    /// </summary>
    public partial class HTMLToolHome : Page
    {
        public HTMLToolHome()
        {
            InitializeComponent();
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            var logic = new Logic();
            var FolderLocation = "";

            if (Location.Text != "Trace Folder Location" || Location.Text != "")
            {
                FolderLocation = Location.Text;
            }

            var results = logic.GetResults(FolderLocation);
            var htmlToolResults = new HTMLToolResults(results);
            this.NavigationService.Navigate(htmlToolResults);
        }

        private void Browse1_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".html",
                Filter = "HTML Files (*.html)|*.html"
            };
            
            // Display OpenFileDialog by calling ShowDialog method 
            var result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                Location.Text = filename;
            }
        }

        private void Clear1_Click(object sender, RoutedEventArgs e)
        {
            Location.Text = "";
        }
    }
}
