using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Input;
using MVVM.Model;

namespace MVVM.ViewModel
{
    public class HTMLToolViewModel
    {
        private IList<HTMLToolModel> _resultList;
        private Logic _logic;

        public HTMLToolViewModel(string folderLoc)
        {
            _resultList = new List<HTMLToolModel>();
            _logic = new Logic(folderLoc);

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
