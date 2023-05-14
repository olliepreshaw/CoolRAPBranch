using KIT206_RAP.Entites;
using KIT206_RAP.Controll;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RAP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void researcherListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (researcherListView.SelectedItem != null)
            {
                Researcher selectedResearcher = (Researcher)researcherListView.SelectedItem;
                Console.WriteLine("selected researcher" + selectedResearcher.FirstName);
                // Do something with the selectedResearcher object
            }
        }
        public MainWindow()
        {
            ObservableCollection<Researcher> researchers = new ObservableCollection<Researcher>(ResearcherControl.FetchResearchers());
            InitializeComponent();
            researcherListView.ItemsSource = researchers;
        }
    }
}
