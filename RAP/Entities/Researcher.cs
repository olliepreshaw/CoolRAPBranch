using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Schema;

namespace KIT206_RAP.Entites

{
   

    internal class Researcher
    {
        // Properties some of these are not necisary
        public int ID { get; set; }
        public ResearcherType Type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string SchoolUnit { get; set; }
        public Campus Camp { get; set; }
        public string Email { get; set; }
        public string photoURL{ get; set; }
        public string CurrentJobTitle { get; set; }
        public DateTime CommenceCurrentPosition { get; private set; }
        public DateTime CommencedWithInstitution { get; private set; }
        public Level PositionLevel { get; set; }
        public double Q1Percentage { get; private set; }
        public List<Publication> Pubs{ get; set; }
        public double Tenure { get; private set; } // time in fractional years since the researcher commecned with the institution
        //public Level positionLevle { get; set; }

        // Constructor
        public Researcher(int iD, string type, string firstName, string lastName, String title, string schoolUnit,
            string campHouse, string email, string photURL, DateTime utas_start, DateTime curretn_start, string lev)
        {
            ID = iD;
            Type = (ResearcherType)Enum.Parse(typeof(ResearcherType), type);
            FirstName = firstName;
            LastName = lastName;
            Title = title;
            SchoolUnit = schoolUnit;
            Email = email;
            photoURL = photURL;
            Pubs = new List<Publication>();
            //Camp = (Campus)Enum.Parse(typeof(Campus), type);
            this.photoURL = photoURL;
            CommencedWithInstitution = utas_start;
            CommenceCurrentPosition = curretn_start;
            PositionLevel = (Level)Enum.Parse(typeof(Level), lev);
            Console.WriteLine(firstName + "..." + PositionLevel);
            // could call the GetPubs here in the constructor if we wanted, get the pubs when we
            // create the researcher if we want...

            //photoPlaceHolder = FetchPhoto();
            //positionLevle = CalcPosLevel(lev);
            //Title = title;
            //DeriveJobTitle(positionLevle);
            }
        // i dont think this is needed
        // delete before submisssion.
        /*
        public Campus CampCalc(string strCamp)
        {
            
            if (strCamp.Equals("Hobart")){
                return Campus.Hobart;
            }else if (strCamp.Equals("Launceston"))
            {
                return Campus.Launceston;
            }
            return Campus.Cradle_Coast;
        }*/

        
        
        // these do not happen in the constructor
        // if the db returns a list of positions with the researcher i say we itterate / loop through them
        // looking for what we need, if not we are calling the DB interface with a different query for each 
        // piece of data we need

        // overload constructor to deal with the position data
        // not sure how this will be defined, do they have poitions from other institutions in this list?
        // if so will have to check that the name matches the institution we are developing for presumably UTAS
        public static void CalcPositionInfo(Researcher researcher, List<Position> positions)
        {
            Console.WriteLine("\t\t\t\tthello in the funciton");
            foreach (Position positiona in positions)
            {
                Console.WriteLine(positiona.StartDate);
            }
            CalcEarliestPos(researcher, positions);
            CalcTenure(researcher, researcher.CommencedWithInstitution);
            //CalcComencedCurrentPos(researcher, positions);

        }
        public static void CalcEarliestPos(Researcher researcher, List<Position> positions)
        {
            //Commenced with institution is the start date of their earliest position.
            // search the positions table for earliest start dat (min)
            // SELECT MIN(start_date) as earliest_start_date
            // FROM positions;
            // find lowest dat in the list

            // find lowest dat in the list
            DateTime lowest = DateTime.Today;
            foreach (Position position in positions)
            {
                if (position.StartDate < lowest)
                {
                    lowest = position.StartDate;
                }
            }
            researcher.CommencedWithInstitution = lowest;
        }
        public static void CalcTenure(Researcher researcher,DateTime CommCurPos)
        {
            //Tenure is the time in (fractional) years since the researcher commenced with the institution.
            // CommWithInstitution - current timeDate;

            TimeSpan difference = DateTime.Now - CommCurPos;
            double years = difference.TotalDays / 365.25;

            researcher.Tenure = years;
        }

        // this will just have to take the staff member (as stu do not have positions) and loop through looking for the pos. with end date == null
        // // then get that pos start date
        public static DateTime CalcComencedCurrentPos(Staff Sta)
        {
            // presume i don't have to handle null pointer / empty list
            // as each staff will have a current position and entry in DB
            foreach (Position pos in Sta.Positions)
            {
                if (pos.EndDate == null)
                {
                    // or return the pos, or whatever we want
                    return pos.StartDate;
                }
            }
            return new DateTime(1,1,1);
        }
        // this funciton assumes all publication were written in the time the researcher has been with this institution
        // assid form that i think it is correct
        public static double CalculatePerformanceByPublication(Researcher researcher)
        {
            // Calculate the number of years since commencement
            int yearsSinceCommencement = DateTime.Now.Year - researcher.CommencedWithInstitution.Year;
            Console.WriteLine("now is ... " + DateTime.Now.Year + "research commecned with insti year " + researcher.CommencedWithInstitution.Year);
      
            Console.WriteLine("yearsSonceComm = " + yearsSinceCommencement);

            // Calculate the total number of publications
            // assumes that all these publications are for this institution
            int totalPublications = researcher.Pubs.Count;
            Console.WriteLine("total Pubs " + researcher.Pubs.Count);
            // Calculate the performance by publication needs to be round to floor
            double performanceByPublication = (double)totalPublications / yearsSinceCommencement;
            double perfByPub = Math.Floor(performanceByPublication);    // cast to int?
            
            return perfByPub;
        }
        public static double AveragePublicationsPerYear(Researcher Res)
        {
            // Get the current date
            DateTime currentDate = DateTime.Today;

            // Filter the publications to only include those from the last three years
            List<Publication> recentPublications = Res.Pubs
                .Where(p => p.AvailabilityDate.Year >= currentDate.Year - 2)
                .ToList();

            // Calculate the total number of publications
            int totalPublications = recentPublications.Count();

            // Calculate the number of years represented in the recent publications
            //int years = currentDate.Year - recentPublications.Last().AvailabilityDate.Year + 1;
            int years = 3;
            // Calculate the average number of publications per year
            double averagePublicationsPerYear = (double)totalPublications / years;
            Console.WriteLine(averagePublicationsPerYear);

            return averagePublicationsPerYear;
        }
        public String FetchPhoto()
        {
            // fetch photo url
            String placeHolder = "placeHolder";

            return placeHolder;
        }
        public enum ResearcherType
        {
            Student,
            Staff
        }
        public enum Campus
        {
            Hobart,
            Launceston,
            Cradle_Coast
        }

    }
}

    
