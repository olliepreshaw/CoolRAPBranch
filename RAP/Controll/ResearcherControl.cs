using KIT206_RAP.DataBase;
using KIT206_RAP.Entites;
using KIT206_RAP.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KIT206_RAP.View;
using System.Windows;
using System.Collections;
using RAP.Entities;
using RAP;
using System.Collections.ObjectModel;

namespace KIT206_RAP.Controll
{
    internal class ResearcherControl
    {
        public static List<Researcher> Researchers { get; private set; }
        public static Researcher CurrentResearcher { get; private set; }
        // UC8
        public static List<Researcher> FetchResearchers()
        {
            List<Researcher> ResList = DBAdapter.GetResearcher();
            //List<Student> StuList = ResList.OfType<Student>().ToList();
            return ResList;

        }
        public static void DisplayResearchers()
        {
            // old version passed list objects for student and staff
            //List<Staff> StaList = DBAdapter.GetStaff();
            //List<Student> StuList = DBAdapter.GetStudent();
            List<Researcher> ResList = DBAdapter.GetResearcher();
            List<Student> StuList = ResList.OfType<Student>().ToList();


            ResearcherView.PrintAllResearchers(ResList);
            //ResearcherView.PrintAllResearchers(StuList, StaList);
        }

        // need positions, not pubs here...
        // positions only for staff, so two researcher details 
        // UC16
        public static void DisplayResearcherDetails(Researcher Res)
        {
            //get the positions here

            if (Res is Staff staff)
            {
                DBAdapter.GetPositions(staff);
            }
            ResearcherDetailsView.DisplayResearcherDetails(Res);
        }

        public static void DisplayPerformanceDetails(Researcher Res)
        {
            // performance details view
             DBAdapter.GetPubs(Res);
            // Console.WriteLine(Res.Type);
            // PublicationView.PrintAllPublication(Res);
            //Console.WriteLine(Res.Type);

            PerformaceDetailsView.PrintPerformanceView(Res);
        }
        public static List<Researcher> FilterLevel(string lev, ObservableCollection<Researcher> researchers)
        {
            if (lev == "All levels")
            {
                // Return all researchers
                return researchers.OrderBy(x => x.LastName).ToList();
            }
            else
            {
                Level level = (Level)Enum.Parse(typeof(Level), lev);
                var filteredResearchers =
                    from entry in researchers
                    where entry.PositionLevel == level
                    select entry;

                return filteredResearchers.OrderBy(x => x.LastName).ToList();
            }
        }


        // can't pass an obersevable collection, so just pass the list and update the obs coll
        // back in the main
        public static void FilterList(List<Researcher> ResList, string searchText)
        {
            

        }

        public static void ControllTheDeetails(Researcher researcher)
        {
            Researcher.Q1PercentageCalc(researcher);

            if (researcher.Type == Researcher.ResearcherType.Staff)
            {
                Staff staff = (Staff)researcher;
                //Staff staff = (Staff)researcherListView.SelectedItem;
                //ResearcherControl.AverageThreeYear(staff, publications);
                Staff.AverageThreeYear(staff);
                Staff.PerfByPub(staff);
                staff.FundingRecieved=XMLAdapter.LoadFunding(staff);
                Staff.PerfByFund(staff);

            }
        }

    }

}

