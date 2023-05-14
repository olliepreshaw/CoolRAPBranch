using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KIT206_RAP.Entites;
using KIT206_RAP.Controll;

namespace KIT206_RAP.View
{
    internal class PerformaceDetailsView
    {
        /*
         *  When the user clicks on the performance details on researcher details view, performance details view will be displayer SW UC27_User_views_Performance
        Performance details view is a new window SWC 27
        Q1 Percentage is the total number of Q1 publications, divided by total number of publications times 100. SWC 27
        3-year Average is the total number of publications in the previous three whole calendar years, divided by three. SWC 27
        Performance by Publication is the average number of publications per year since commencement. SWC 27
        Performance by Funding Received is the average amount of funding per year since commencement. SWC 27
        Supervisions is the number of students the staff member is currently or has previously supervised. SWC 27
        The display will show the following performance details, some of which are only available for staff and others only for students:
        Name (both); Title (both); School/Unit (both); Campus (both); Email (both); Q1 Percentage (both); 3-year Average (staff); Funding
        Received (staff); Performance by Publication (staff); and Performance by Funding Received (staff).

        The display should have labels for each piece of information, with the values for 3-year average, Funding Received, Performance by
        Publication, Performance by Funding Received and Supervisions left blank for students, and the values for Degree and Supervisor
        left blank for staff.

        When displaying details for a staff member, the Researcher Details view will show that staff member’s performance, which is their
        three-year average for publications divided by the expected number of publications given their employment level, expressed as a
        percentage with one decimal place shown; performance by publication, which is expressed as an average per year with an integer
        value (rounded to the floor in case of a decimal); performance by funding, which is expressed as an average per year with one
        decimal place shown. 
         */
        public static void PrintPerformanceView(Researcher Res)
        {
            //Researcher.Q1PercentageCalc(researcher, publications);

            Console.WriteLine("\t#####\t#####\t WELCOME TO PERFORMANCE WIEW \t########\t#######");
            Console.WriteLine("Performance for :" + Res.LastName);
            Console.WriteLine("Q1 performance is ... un-available at the moment" + "%");
            // Q1 unabaliable, no Q ratings in DB
            // Performance by Funding Received unavailable, as no funding in DB
            // these should probably be called by the controller then stored in attributes fo rthe staff
            Console.WriteLine("The three year average is... " + Researcher.AveragePublicationsPerYear(Res));
            Console.WriteLine("The performance by Publication is " + Researcher.CalculatePerformanceByPublication(Res));

            // have not implimented supervisions yet... will need new DB query

            Console.WriteLine("Pause");
        }
    }
}
