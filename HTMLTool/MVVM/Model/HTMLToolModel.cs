using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM.Model
{
    public class HTMLToolModel : INotifyPropertyChanged
    {
        private string _direction;
        private double _average;
        private double _max;
        private double _min;
        private int _count;

        public string Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                _direction = value;
                OnPropertyChanged("Direction");
            }
        }
        public double Average
        {
            get
            {
                return _average;
            }
            set
            {
                _average = value;
                OnPropertyChanged("Average");
            }
        }
        public double Max
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
                OnPropertyChanged("Max");
            }
        }
        public double Min
        {
            get
            {
                return _min;
            }
            set
            {
                _min = value;
                OnPropertyChanged("Min");
            }
        }
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
                OnPropertyChanged("Count");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
