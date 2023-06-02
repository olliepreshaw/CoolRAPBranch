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
