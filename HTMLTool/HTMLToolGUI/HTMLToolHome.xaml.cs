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
            var fileList = new List<string>();

            if (Location1.Text != "Sink Node Trace Location (required)" || Location1.Text != "")
            {
                fileList.Add(Location1.Text);
            }

            if (Location2.Text != "Overflow of Sink Node Trace Location (optional)" || Location1.Text != "")
            {
                fileList.Add(Location2.Text);
            }

            if (Location3.Text != "Overflow of Sink Node Trace Location (optional)" || Location1.Text != "")
            {
                fileList.Add(Location3.Text);
            }

            if (Location4.Text != "Overflow of Sink Node Trace Location (optional)" || Location1.Text != "")
            {
                fileList.Add(Location4.Text);
            }

            var results = logic.GetResults(fileList);
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
                Location1.Text = filename;
            }
        }

        private void Browse2_Click(object sender, RoutedEventArgs e)
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
                Location2.Text = filename;
            }
        }

        private void Browse3_Click(object sender, RoutedEventArgs e)
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
                Location3.Text = filename;
            }
        }

        private void Browse4_Click(object sender, RoutedEventArgs e)
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
                Location4.Text = filename;
            }
        }

        private void Clear1_Click(object sender, RoutedEventArgs e)
        {
            Location1.Text = "";
        }
        private void Clear2_Click(object sender, RoutedEventArgs e)
        {
            Location2.Text = "";
        }
        private void Clear3_Click(object sender, RoutedEventArgs e)
        {
            Location3.Text = "";
        }
        private void Clear4_Click(object sender, RoutedEventArgs e)
        {
            Location4.Text = "";
        }
    }
}
