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
using System.Windows.Shapes;

using KIT206_RAP.DataBase;
using KIT206_RAP.Controll;
using KIT206_RAP.Entites;
using KIT206_RAP.View;

namespace RAP
{
    /// <summary>
    /// Interaction logic for PerformanceDetailsWindow.xaml
    /// </summary>
    public partial class PerformanceDetailsWindow : Window
    {
        public PerformanceDetailsWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RPerformance.DataContext = Name;

        }
    }
}
