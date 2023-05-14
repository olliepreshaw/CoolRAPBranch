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
    /*
     *It will show the following basic details, some of which are only available for staff and others only for students:
     *Name (both); 
     *Title (both); 
     *School/Unit (both); 
     *Campus (both); 
     *Email (both); 
     *Photo (both); 
     *Current Job Title (both); 
     *Commenced with the institution (both);
     *Commenced current position (both); 
     *Tenure (both); 
     *Publications (both);
     *Supervisions (staff);
     *Degree (students); Supervisor (students),
        and Performance details button

        Photo is stored as the URL of an image, but must be presented as an image. SWC 16
        Current Job Title is a label derived from their current position's level. SWC 16
        Commenced with institution is the start date of their earliest position. SWC 16
        Commenced current position is the start date of their current position. SWC 16
        Tenure is the time in (fractional) years since the researcher commenced with the institution. SWC 16
        It would enhance the application if the Researcher Details view also includes a table of a staff member’s previous positions and
        those positions' start and end dates (not required for students).

        The expected number of publications per year for each level is as follows: A, 0.5; B, 1; C, 2; D, 3.2; E, 4. SWC 
     */
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
            Console.WriteLine("Photo: " + Res.photoURL);
            Console.WriteLine("Current Job Title" + Res.CurrentJobTitle);
            Console.WriteLine("Commenced with Institution: " + Res.CommencedWithInstitution);
            Console.WriteLine("Commecnce curr Pos: " + Res.CommenceCurrentPosition);
            //Console.WriteLine("Tenure: " + student.Tenure);
            //Console.WriteLine("Tenure: " + Res.Supervisor);
            if (Res is Staff staff)
            {
                // display specific student stuff... they have a degree and a super, but not type???
                // display supervisions, this loop should be moved?
                Console.WriteLine("LINQ Statements....");
                Console.WriteLine("supervisions are");
                staff.Supervisions.ForEach(res => Console.WriteLine($"Supervises {res.FirstName} {res.LastName}"));
                Console.WriteLine("Stff memb positions are.... ");
                staff.Positions.ForEach(pos => Console.WriteLine($"{pos.StartDate} {pos.EndDate}"));

                /*
                foreach (Researcher res in staff.Supervisions)
                {
                    Console.WriteLine($"Supervises {res.FirstName} {res.LastName}");
                }
                Console.WriteLine("Stff memb positions are.... ");
                foreach (Position pos in staff.Positions)
                {
                    Console.WriteLine(pos.StartDate + " " + pos.EndDate);
                }
                */

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
