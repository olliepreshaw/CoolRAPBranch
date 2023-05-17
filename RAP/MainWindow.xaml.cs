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
        private ObservableCollection<Publication> selectedResearcherPublications;
        private ObservableCollection<Researcher> researchers;

        public BitmapImage ImageData { get; set; }


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
                // researcher details
                name.Text = "Name: " + selectedResearcher.FirstName + " " + selectedResearcher.LastName;
                //title not displaying at all, mustn't be pulling from db?
                title.Text = "Current Job Title: " + selectedResearcher.Title;
                unit.Text = "Unit: " + selectedResearcher.SchoolUnit;
                Enum campusPr = selectedResearcher.Camp;
                campus.Text = "Campus: " + campusPr.ToString();
                email.Text = "Email: " + selectedResearcher.Email;
                //current job title not working, not sure its calculating?
                job.Text = "Job: " + selectedResearcher.Job_Title;
                
                commencedInt.Text = "Commenced with institution: " + selectedResearcher.CommencedWithInstitution.ToString("d");
                
                commencedCurr.Text = "Commenced current job: " + selectedResearcher.CommenceCurrentPosition.ToString("d");
                prevPos.Text = "Previous positions: " + selectedResearcher;
                //double currTenure = CalcTenure(selectedResearcher, selectedResearcher.CommenceCurrentPosition);
                //tenure.Text = "Tenure: " +  currTenure;
                publi.Text = "Publications: " + selectedResearcher.Pubs.Count;
                //implement this chris or ill cry
                //threeYearAvg.Text = "3-year-average: " + selectedResearcher;
                //cummulative count of sups pls, can't seem to access supervisions?
                //supervisions.Text = "Job: " + selectedResearcher.;
                //performance.Text = "Performance: " + selectedResearcher.performancebypublication;
                if (selectedResearcher is Student student)
                {
                    //do student stuff...

                    //can't access student specific fields :(((
                    //degree.Text = "Degree: " + selectedResearcher.degree;
                    supervisor.Text = "Supervisor: " + student.Supervisor;
                    degree.Text = "Degree: " + student.Degree;
                }
                // image
                ImageData = new BitmapImage(new Uri(selectedResearcher.PhotoURL));

                Console.WriteLine("image URL " + selectedResearcher.PhotoURL);

                Console.WriteLine("Selected researcher: " + selectedResearcher.FirstName);
            }
        }
        private void tempButton_Click(object sender, RoutedEventArgs e)
        {
            Researcher selectedResearcher = (Researcher)researcherListView.SelectedItem;
            List<Publication> publications = PublicationsControl.FetchPublications(selectedResearcher);
            PerformanceDetailsWindow PdetailsView = new PerformanceDetailsWindow();
            Researcher.Q1PercentageCalc(selectedResearcher, publications);

            if (selectedResearcher.Type == Researcher.ResearcherType.Staff)
            {
                Staff staff = (Staff)researcherListView.SelectedItem;
                staff.AverageThreeYear(publications);
            }
            PdetailsView.DataContext = selectedResearcher;
            PdetailsView.Show();
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
