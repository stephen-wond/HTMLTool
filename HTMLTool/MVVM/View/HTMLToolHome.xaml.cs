using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using MVVM.Model;

namespace MVVM.View
{
    /// <summary>
    /// Interaction logic for HTMLToolHome.xaml
    /// </summary>
    public partial class HTMLToolHome : Page
    {
        private readonly BackgroundWorker _backgroundWorker;
        private List<HTMLToolModel> _results = new List<HTMLToolModel>();
        private String _folderLocation = "";

        public HTMLToolHome()
        {
            InitializeComponent();

            //background process
            _backgroundWorker = (BackgroundWorker) this.Resources["BackgroundWorker"];

            //progress reporting
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.ProgressChanged += _backgroundWorker_ProgressChanged;

            //background cancelation
            _backgroundWorker.WorkerSupportsCancellation = true;
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Location.Text != "Trace Folder Location" || Location.Text != "")
            {
                _folderLocation = Location.Text;
            }

            _backgroundWorker.RunWorkerAsync(_folderLocation);

            ExecuteButton.IsEnabled = !_backgroundWorker.IsBusy;
            CancelButton.IsEnabled = _backgroundWorker.IsBusy;
            Rectangle1.Visibility = Visibility.Visible;
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _backgroundWorker.CancelAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string folderLocation = (string) e.Argument;
            //here we need to sperate out all the functions into seperate calls 
            //_results = logic.GetResults(folderLocation);
            //process files
            //add to list
            //create an object and perform all actions on this object
            //compare times for file one 25%
            //compare times for file two 50%
            //will also allow for use of cancelations
            e.Result = _results;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var htmlToolResults = new HTMLToolResults((List<HTMLToolModel>)e.Result);

            ExecuteButton.IsEnabled = _backgroundWorker.IsBusy;
            CancelButton.IsEnabled = !_backgroundWorker.IsBusy;
            Rectangle1.Visibility = Visibility.Hidden;

            this.NavigationService.Navigate(htmlToolResults);
        }

        private void _backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
