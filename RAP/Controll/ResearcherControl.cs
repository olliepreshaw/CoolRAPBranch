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
using System.Xml.Linq;
using System.Runtime.Versioning;
using static System.Windows.Forms.AxHost;

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
                //DBAdapter.GetStudentSupervised(staff);
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


        public static List<Researcher> FilterLevel(string lev, ObservableCollection<Researcher> res)
        {
            if (lev == "All levels")
            {
                // Return all researchers
                return res.OrderBy(x => x.LastName).ToList();
            }
            else
            {
                Level level = (Level)Enum.Parse(typeof(Level), lev);
                var filteredResearchers =
                    from entry in res
                    where entry.PositionLevel == level
                    select entry;

                return filteredResearchers.OrderBy(x => x.LastName).ToList();
            }
        }


        // can't pass an obersevable collection, so just pass the list and update the obs coll
        // back in the main
        public static List<Researcher> FilterList(ObservableCollection<Researcher> ResList, string searchText)
        {
            // List to return. Temp strings to handle case
            List<Researcher> filteredList = new List<Researcher>();
            string tempFirstName;
            string tempLastName;
            searchText = searchText.ToLower();

            // Loops list searching for matches
            foreach (Researcher researcher in ResList)
            {
                // Handling case
                tempFirstName = researcher.FirstName.ToLower();
                tempLastName = researcher.LastName.ToLower();
                
                // Finding a match adds result to return list
                if (tempFirstName.Contains(searchText) || tempLastName.Contains(searchText))
                {
                    filteredList.Add(researcher);
                }
            }
            return filteredList;
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

        public static List<Staff> FilterReport(string lev, ObservableCollection<Researcher> researchers)
        {
            List<Staff> filteredStaff = new List<Staff>();      // Return List
            Staff staff;                                        // Conversion for Researcher -> Staff

            // Loops Full list
            foreach (Researcher res in researchers)
            {
                // Matches Staff types
                if (res.Type == Researcher.ResearcherType.Staff)
                {
                    staff = (Staff)res;     // Type Conversion

                    // Setup Data. Can be done elsewhere?
                    staff.Pubs = DBAdapter.GetPubs(staff);
                    Staff.AverageThreeYear(staff);
                    Staff.PerfByPub(staff);

                    // Converts string PerfByPub into a double for math comparisions
                    double.TryParse(staff.PerformanceByPublication.Replace("%", ""), out double result);

                    // Determine which report it should be added to
                    switch (lev)
                    {
                        case "Poor":
                            if (result <= 70)
                            {
                                filteredStaff.Add(staff);
                            }
                            break;
                        case "Below Expectations":
                            if (result > 70 && result < 110)
                            {
                                filteredStaff.Add(staff);
                            }
                            break;
                        case "Meeting Minimum":
                            if (result >= 110 && result < 200)
                            {
                                filteredStaff.Add(staff);
                            }
                            break;
                        case "Star Performers":
                            if (result >= 200)
                            {
                                filteredStaff.Add(staff);
                            }
                            break;
                    }
                }
            }
            return filteredStaff;
        }

    }

}

