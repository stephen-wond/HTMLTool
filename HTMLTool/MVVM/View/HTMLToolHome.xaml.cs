using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using MVVM.Model;
using MVVM.ViewModel;

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
        private HTMLToolViewModel _vm = new HTMLToolViewModel();

        public HTMLToolHome()
        {
            InitializeComponent();

            //background process
            _backgroundWorker = (BackgroundWorker) this.Resources["BackgroundWorker"];
            
            //background cancelation
            _backgroundWorker.WorkerSupportsCancellation = true;
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            HTMLToolViewModel.Execute();
        }

        private void Browse1_Click(object sender, RoutedEventArgs e)
        {
            HTMLToolViewModel.Browse();
        }

        private void Clear1_Click(object sender, RoutedEventArgs e)
        {
            Location.Text = "";
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            HTMLToolViewModel.Cancel();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            HTMLToolViewModel.DoWork();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            HTMLToolViewModel.Complete();
        }
    }
}
