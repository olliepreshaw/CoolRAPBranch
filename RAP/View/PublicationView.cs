using KIT206_RAP.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_RAP.View
{
    internal class PublicationView
    {

        public static void PrintAllPublication(Researcher Res)
        {
            Console.WriteLine("---\t---\t---\t We are in the Publicaion View \t---\t---\t---");
            foreach (Publication publication in Res.Pubs)
            {
                 // could send this to an overloaded print fuction in the publication class if we wanted.
                Console.WriteLine("Publicaiton Name: " + publication.Title);
            }
            Console.WriteLine("pause");
        }
    }
}
