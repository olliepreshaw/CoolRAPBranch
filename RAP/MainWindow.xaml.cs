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
using RAP.Controll;

namespace RAP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum Level
        {
            Student,
            A,
            B,
            C,
            D,
            E
        }
        private ObservableCollection<Publication> selectedResearcherPublications;
        private ObservableCollection<Researcher> researchers;


        // select a researcher
        private void researcherListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (researcherListView.SelectedItem != null)
            {
                Researcher selectedResearcher = (Researcher)researcherListView.SelectedItem;
                List<Publication> publications = PublicationsControl.FetchPublications(selectedResearcher);

                selectedResearcherPublications.Clear();
                foreach (var publication in publications)
                {
                    selectedResearcherPublications.Add(publication);
                }

                Console.WriteLine("Selected researcher: " + selectedResearcher.FirstName);
            }
        }

        private void PublicationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PublicationListView.SelectedItem != null)
            {
                Publication selectedPublication = (Publication)PublicationListView.SelectedItem;

                // Do something with the selected publication
            }
        }

        public MainWindow()
        {
            researchers = new ObservableCollection<Researcher>(ResearcherControl.FetchResearchers());
            selectedResearcherPublications = new ObservableCollection<Publication>();
            InitializeComponent();
            researcherListView.ItemsSource = researchers;
            PublicationListView.ItemsSource = selectedResearcherPublications;
        }
    }
}
