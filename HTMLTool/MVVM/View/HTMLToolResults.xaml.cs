using System.Collections.Generic;
using System.Windows.Controls;
using MVVM.Model;
using MVVM.ViewModel;

namespace MVVM.View
{
    /// <summary>
    /// Interaction logic for HTMLToolResults.xaml
    /// </summary>
    public partial class HTMLToolResults : Page
    {
        public HTMLToolResults(HTMLToolViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
