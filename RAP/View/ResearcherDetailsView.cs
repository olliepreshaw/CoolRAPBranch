using KIT206_RAP.Controll;
using KIT206_RAP.DataBase;
using KIT206_RAP.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_RAP.View
{
    
    internal class ResearcherDetailsView
    {
        public static void DisplayResearcherDetails(Researcher Res)
        {

            //DBAdapter.GetPubs(student);

            Console.WriteLine("---\t---\tWelcome to Researcher Details View\t---\t---");
            Console.WriteLine("---\t---\tYou Have Selected a Student\t---\t---");

            Console.WriteLine("Name: " + Res.FirstName + " " + Res.LastName);
            //Console.WriteLine("Tite: " + student.Title);
            Console.WriteLine("School/Unit: " + Res.SchoolUnit);
            Console.WriteLine("Campus: " + Res.Camp);
            Console.WriteLine("Email: " + Res.Email);
            Console.WriteLine("Photo: " + Res.PhotoURL);
            Console.WriteLine("Current Job Title" + Res.CurrentJobTitle);
            Console.WriteLine("Commenced with Institution: " + Res.CommencedWithInstitution);
            Console.WriteLine("Commecnce curr Pos: " + Res.CommenceCurrentPosition);
            //Console.WriteLine("Tenure: " + student.Tenure);
            //Console.WriteLine("Tenure: " + Res.Supervisor);
            if (Res is Staff staff)
            {
                Console.WriteLine("LINQ Statements....");
                Console.WriteLine("supervisions are");
                Console.WriteLine("Stff memb positions are.... ");
                staff.Positions.ForEach(pos => Console.WriteLine($"{pos.StartDate} {pos.EndDate}"));


            }
            if (Res is Student student)
            {
                Console.WriteLine($"Student {student.FirstName} {student.LastName}, Supervisor {student.Supervisor}");
            }

            Console.WriteLine("PAUSE");
            Console.WriteLine("PAUSE");
            Console.WriteLine("PAUSE");

        }

    }
}
