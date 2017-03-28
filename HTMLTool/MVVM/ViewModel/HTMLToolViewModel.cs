using System;
using System.Collections.Generic;
using System.Windows.Input;
using MVVM.Model;

namespace MVVM.ViewModel
{
    public class HTMLToolViewModel
    {
        private IList<HTMLToolModel> _resultList;
        public HTMLToolViewModel()
        {
            _resultList = new List<HTMLToolModel> {new HTMLToolModel {Direction = "Test1", Average = 10, Count = 10, Max = 10, Min = 10}, new HTMLToolModel { Direction = "Test2", Average = 10, Count = 10, Max = 10, Min = 10 } };
        }

        public IList<HTMLToolModel> Results
        {
            get { return _resultList; }
            set { _resultList = value; }
        }

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
