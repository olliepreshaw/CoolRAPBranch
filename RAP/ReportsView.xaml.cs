using KIT206_RAP.Controll;
using KIT206_RAP.Entites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace RAP
{
    /// <summary>
    /// Interaction logic for ReportsView.xaml
    /// </summary>
    public partial class ReportsView : Window
    {
        private ObservableCollection<Researcher> staff;
        public ReportsView(ObservableCollection<Researcher> staff)
        {
            InitializeComponent();
            this.staff = staff;
            StaffListView.ItemsSource = staff;
        }
        private void ReportSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            string selectedLevel = selectedItem.Content.ToString();
            StaffListView.ItemsSource = ResearcherControl.FilterReport(selectedLevel, staff);
        }
    }
    
}
