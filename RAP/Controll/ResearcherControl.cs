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
using MySqlX.XDevAPI.Common;
using System.CodeDom.Compiler;
using System.Windows.Navigation;

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
        public static void DisplayResearcherDetails(Researcher Res, List<Researcher> resList)
        {
            Console.WriteLine("in display Researcher Details");
            if (Res is Staff staff)
            {
                DBAdapter.GetPositions(staff);

                findSupervisions(resList, staff);
            }if (Res is Student stu)
            {

                Console.WriteLine("res is student");
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
                Staff.AverageThreeYear(staff);
                Staff.PerfByPub(staff);
                staff.FundingRecieved = XMLAdapter.LoadFunding(staff);
                Staff.PerfByFund(staff);
            }
        }

        public static string findSupervisions(List<Researcher> researchers, Staff stf)
        {
            Console.WriteLine("finding supivistions");
            int supervisorID = stf.ID; // Replace stf.ID with the staff member's ID

            var matchingStudents = researchers
                .Where(res => res is Student)
                .Cast<Student>()
                .Where(stu => stu.Supervisor == supervisorID);
            string studentNames = string.Join(", ", matchingStudents.Select(stu => stu.FirstName + " " + stu.LastName));
            foreach (var student in matchingStudents)
            {
                // Access the properties of each matching student
                Console.WriteLine("Matching student found: " + student.FirstName + " " + student.LastName);
            }
            stf.SuperCount = matchingStudents.Count();
            stf.StudentsSupervised = studentNames;

            Console.WriteLine("the student names are.///");
            Console.WriteLine(studentNames);

            return studentNames;
        }


        public static List<List<Staff>> SortReport(ObservableCollection<Researcher> researchers)
        {
            List<Staff> Poor = new List<Staff>();
            List<Staff> B_Expect = new List<Staff>();
            List<Staff> M_Min = new List<Staff>();
            List<Staff> Stars = new List<Staff>();
            List<List<Staff>> results = new List<List<Staff>>();
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
                    staff.FundingRecieved = XMLAdapter.LoadFunding(staff);

                    // Converts string PerfByPub into a double for math comparisions
                    double.TryParse(staff.PerformanceByPublication.Replace("%", ""), out double result);

                    // Determine which report it should be added to
                    if (result <= 70.0)
                    {
                        Poor.Add(staff);
                    }
                    else if (result > 70.0 && result < 110.0)
                    {
                        B_Expect.Add(staff);
                    }
                    else if (result >= 110.0 && result < 200.0)
                    {
                        M_Min.Add(staff);
                    }
                    else
                    {
                        Stars.Add(staff);
                    }
                }
            }
            results.Add(Poor);
            results.Add(B_Expect);
            results.Add(M_Min);
            results.Add(Stars);
            return results;
        }

        public static List<Staff> GenReport(string lev, List<List<Staff>> data)
        {
            switch (lev)
            {
                case "Poor":
                    return data[0].OrderBy(x=>x.PerformanceByPublication).ToList();
                    break;
                case "Below Expectations":
                    return data[1].OrderBy(x => x.PerformanceByPublication).ToList();
                    break;
                case "Meeting Minimum":
                    return data[2].OrderByDescending(x => x.PerformanceByPublication).ToList();
                    break;
                case "Star Performers":
                    return data[3].OrderByDescending(x => x.PerformanceByPublication).ToList();
                    break;
            }
            return null;
         }

    }

}

