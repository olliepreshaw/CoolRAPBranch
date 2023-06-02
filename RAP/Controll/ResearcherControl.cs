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
        public static ObservableCollection<Researcher> FilterName(string name, ObservableCollection<Researcher> res)
        {
            var filteredCollection = new ObservableCollection<Researcher>();
            String query = name.ToUpper();
            if (query != "")
            {
                var SelectQuery2 = from entry in res
                                   where (entry.FirstName.ToUpper().Contains(name)
                                         || entry.LastName.ToUpper().Contains(name))
                                   select entry;

                List<Researcher> tempList = SelectQuery2.ToList();
                
                tempList.OrderBy(x => x.LastName);
                filteredCollection = new ObservableCollection<Researcher>(tempList);
            }

                return filteredCollection;

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

           public static void ControllTheDeetails(Researcher researcher)
        {
            Researcher.Q1PercentageCalc(researcher);

            if (researcher.Type == Researcher.ResearcherType.Staff)
            {
                Staff staff = (Staff)researcher;
                Staff.AverageThreeYear(staff);
                Staff.PerfByPub(staff);
                staff.FundingRecieved=XMLAdapter.LoadFunding(staff);
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


    }

}

