using KIT206_RAP.DataBase;
using KIT206_RAP.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KIT206_RAP.View;

namespace KIT206_RAP.Controll
{
    internal class ResearcherControl
    {
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

        public static List<Researcher> FilterLevel(Level level, List<Researcher> ResList)
        {

            return ResList;


        }


    }

}

