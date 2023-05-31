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
using RAP.Entities;


// This is a comment, welcom to the word of git
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
                DateTime now = DateTime.Today;
                

                selectedResearcherPublications.Clear();
                foreach (var publication in publications)
                {
                    selectedResearcherPublications.Add(publication);
                }
                // researcher details
                name.Text = "Name: " + selectedResearcher.FirstName + " " + selectedResearcher.LastName;
                //title not displaying at all, mustn't be pulling from db?
                resTitle.Text = "Title: " + selectedResearcher.Title;
                unit.Text = "Unit: " + selectedResearcher.SchoolUnit;
                Enum campusPr = selectedResearcher.Camp;
                campus.Text = "Campus: " + campusPr.ToString();
                email.Text = "Email: " + selectedResearcher.Email;
                //current job title not working, not sure its calculating?
                job.Text = "Job: " + selectedResearcher.Job_Title;
                
                commencedInt.Text = "Commenced with institution: " + selectedResearcher.CommencedWithInstitution.ToString("d");
                
                commencedCurr.Text = "Commenced current job: " + selectedResearcher.CommenceCurrentPosition.ToString("d");
                prevPos.Text = "Previous positions: " + selectedResearcher;
                tenure.Text = "Tenure: " + Math.Round(((now - selectedResearcher.CommencedWithInstitution).TotalDays)/365, 2) + " years";
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
                else if (selectedResearcher is Staff staff)
                {
                    threeYearAvg.Text = "3-year-average: " + staff.ThreeYearAverage;
                    performanceFund.Text = "Performance by Funding: " + "$" + (Math.Round(staff.FundingRecieved / ((DateTime.Now - staff.CommencedWithInstitution).TotalDays / 365), 1)).ToString();
                    performancePub.Text = "Performance by Funding: " + String.Format("{0:0.0}", Math.Round(staff.ThreeYearAverage / staff.ExpectedNoPubs * 100, 1) + "%");
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
            PerformanceDetailsWindow PdetailsView = new PerformanceDetailsWindow();

            ResearcherControl.ControllTheDeetails(selectedResearcher);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(selectedResearcher.PhotoURL);
            image.EndInit();
            PdetailsView.profilePic.Source = image;

            PdetailsView.DataContext = selectedResearcher;
            PdetailsView.Show();
        }

        private void PublicationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PublicationListView.SelectedItem != null)
            {
                Publication selectedPublication = (Publication)PublicationListView.SelectedItem;


                DOI.Text = "Title : " + selectedPublication.DOI;
                pubTitle.Text = "Publication Title: " + selectedPublication.Title;
                authors.Text = "Authors: " + selectedPublication.Authors;
                //pubYear.Text = "Publication Year: " + selectedPublication.year;
                ranking.Text = "Ranking: " + selectedPublication.Ranking;
                pubType.Text = "Publication Type: " + selectedPublication.Type;
                citeAS.Text = "Cite As: " + selectedPublication.CiteAs;
                avaDate.Text = "Availability Date: " + selectedPublication.AvailabilityDate;
                //(EndDate - StartDate).TotalDays
                DateTime now = DateTime.Today;
                pubAge.Text = "Publication Age: " + (now -selectedPublication.AvailabilityDate).TotalDays + " days";

                // Do something with the selected publication
                //< TextBlock Name = "DOI" Text = "DOI: " FontSize = "10" Margin = "2" />
                //< TextBlock Name = "pubTitle" Text = "Title: " FontSize = "10" Margin = "2" />
                //< TextBlock Name = "authors" Text = "Authors: " FontSize = "10" Margin = "2" />
                //< TextBlock Name = "pubYear" Text = "Publication Year: " FontSize = "10" Margin = "2" />
                //< TextBlock Name = "ranking" Text = "Ranking: " FontSize = "10" Margin = "2" />
                //< TextBlock Name = "pubType" Text = "Publication Type: " FontSize = "10" Margin = "2" />
                //< TextBlock Name = "citeAS" Text = "Cite A: " FontSize = "10" Margin = "2" />
                //< TextBlock Name = "avaDate" Text = "Availability Date: " FontSize = "10" Margin = "2" />
                //< TextBlock Name = "pubAge" Text = "Age: " FontSize = "10" Margin = "2" />
            }
        }

        public MainWindow()
        {
            researchers= new ObservableCollection<Researcher>(ResearcherControl.FetchResearchers());
            selectedResearcherPublications = new ObservableCollection<Publication>();
            InitializeComponent();
            researcherListView.ItemsSource = researchers;
            PublicationListView.ItemsSource = selectedResearcherPublications;
            
        }
                private void LevelFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            string selectedLevel = selectedItem.Content.ToString();

            researcherListView.ItemsSource = ResearcherControl.FilterLevel(selectedLevel, researchers);
            

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
