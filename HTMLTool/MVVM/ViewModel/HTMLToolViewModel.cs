using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Input;
using MVVM.Model;
using MVVM.View;

namespace MVVM.ViewModel
{
    public class HTMLToolViewModel
    {
        private IList<HTMLToolModel> _resultList;

        public HTMLToolViewModel()
        {
            _resultList = new List<HTMLToolModel>();
        }

        public void CallLogic(string folderLoc)
        {
            var _logic = new Logic(folderLoc);

            _resultList.Add(_logic.GetResultsBetween(1));
            _resultList.Add(_logic.GetResultsBetween(2));
            _resultList.Add(_logic.GetResultsBetween(3));
            _resultList.Add(_logic.GetResultsBetween(4));
        }

        public IList<HTMLToolModel> Results
        {
            get { return _resultList; }
            set { _resultList = value; }
        }

        public static void Browse()
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

        public static void Execute()
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

        public static void Cancel()
        {
            _backgroundWorker.CancelAsync();
        }

        public static void DoWork()
        {
            string folderLocation = (string)e.Argument;
            _vm.CallLogic(folderLocation);
            e.Result = _results;
        }

        public static void Complete()
        {
            var htmlToolResults = new HTMLToolResults(_vm);

            ExecuteButton.IsEnabled = _backgroundWorker.IsBusy;
            CancelButton.IsEnabled = !_backgroundWorker.IsBusy;
            Rectangle1.Visibility = Visibility.Hidden;

            this.NavigationService.Navigate(htmlToolResults);
        }

        public static void Progress()
        { }

        private ICommand _mUpdater;

        public ICommand UpdateCommand
        {
            get { return _mUpdater ?? (_mUpdater = new Updater()); }
            set { _mUpdater = value; }
        }

        private class Updater : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
            }
        }
    }
}
