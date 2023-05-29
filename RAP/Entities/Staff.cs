using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KIT206_RAP.Entites.Staff;
/*
3-year Average is the total number of publications in the previous three whole calendar years, divided by three. SWC 27
Performance by Publication is the average number of publications per year since commencement. SWC 27
Performance by Funding Received is the average amount of funding per year since commencement. SWC 27
Supervisions is the number of students the staff member is currently or has previously supervised. 
*/
namespace KIT206_RAP.Entites
{
    public class Staff : Researcher
    {
        
        //public double ExpectedNoPubs { get; set; }
        //public JobTitle Job_Title{ get; set; }
        public int FundingRecieved { get; set; }
        public string PerformanceByFunding { get; set; }
        public double ThreeYearAverage { get; set; }
        public string PerformanceByPublication { get; set; }
        public List<Position> Positions { get; set; }
        // not sure how to handle supervisions, just the researcher id or a name?
        public int SuperCount { get; set; }
        public List<Student> Supervisions { get; set; }
        // want to make this an enum
        public string level { get; set; }

            
       // Constructor
        public Staff(int id, string type, string firstName, string lastName, string title, string schoolUnit, string campHouse, string email, string photURL, string lev, DateTime utas_start, DateTime curretn_start)
            : base( id, type, firstName, lastName, title, schoolUnit, campHouse, email, photURL, utas_start, curretn_start, lev)
        {
            // will have to ping the database with something like get all students with "researcher name" as supervisor
            // this currently happens in the 
            level = lev;
            Positions = new List<Position>();
            Supervisions = new List<Student>();
            //ThreeYearAverage = AverageThreeYear();
            //double funding = 100;

        }

        

        public static void CalcStafPerformance(Staff Sta)
        {

        }


        // calculate performance by funding
        public double PerforByFund(double fundingRecieved)
        {
            //Performance by Funding Received is the average amount of funding per year since commencement.

            double performaceFunding = 100;
            return performaceFunding;

        }

        public static void AverageThreeYear(Staff staff)
        {
            //3-year Average is the total number of publications in the previous three whole calendar years, divided by three.
            double count = 0;
            DateTime threeYearsAgo = DateTime.Now.AddYears(-3);
            foreach (Publication publication in staff.Pubs)
            {
                if (publication.AvailabilityDate >= threeYearsAgo)
                {
                    count++;
                }
            }
            staff.ThreeYearAverage = Math.Round(count > 0 ? count / 3 : 0, 1);
        }

        public static void PerfByPub(Staff staff)
        {
            staff.PerformanceByPublication = String.Format("{0:0.0}", Math.Round(staff.ThreeYearAverage / staff.ExpectedNoPubs*100,1) +"%");
        }

        public static void PerfByFund(Staff staff)
        {
            staff.PerformanceByFunding = "$"+(Math.Round(staff.FundingRecieved / ((DateTime.Now - staff.CommencedWithInstitution).TotalDays/365), 1)).ToString();
        }

        public static void supervisions()
        {
            // this will probably have to be it's own SQL / LINQ query as we have
            // question each student table for it's supervisor
            // not sure how supervisor is recorded in the DB, maybe they have ID's???
            // FROM students
            // GET *
            // WHERE supervisor = Researcher.lastName


        }
        //supervisions


        // calculate performance
        // calculate perfomeance by publication

        /*
              public static void Q1PercentageCalc(Researcher researcher, List<Publication> publications)
              {
                  int q1Count = 0;
                  int totalPublications = publications.Count;

                  foreach (Publication publication in publications)
                  {
                      if (publication.Ranking == RankingType.Q1)
                      {
                          q1Count++;
                      }
                  }

                  double percentage = (double)q1Count / totalPublications * 100;
                  // sets the top global var...
                  researcher.Q1Percentage = percentage;
                  Console.WriteLine(researcher.Q1Percentage);
                  Console.WriteLine(researcher.Q1Percentage);
                  Console.WriteLine(researcher.Q1Percentage);
              }
              */
       
        
         //* struggling to get below enum working
        public enum ExpectedPublications 
        {
            A,
            B,
            C,
            D,
            E
        }
        

    }
}
