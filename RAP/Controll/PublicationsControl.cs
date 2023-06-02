using KIT206_RAP.DataBase;
using KIT206_RAP.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace RAP.Controll
{
    internal class PublicationsControl
    {

        public static List<Publication> invert_sort(List<Publication> pubList)
        {   
            pubList.Reverse();
            return pubList;
        }
 

        public static List<Publication> sort_list(List<Publication> pubList)
        {   
            List<Publication> orderedList = pubList.OrderByDescending(item => item.AvailabilityDate.Year)
                              .ThenBy(item => item.Title, StringComparer.OrdinalIgnoreCase)
                              .ToList();
            return orderedList;

        }
        public static List<Publication> FetchPublications(Researcher res)
        {
            if(res.Pubs.Count > 0)
            {
                return null;
            }
            else
            {
            List <Publication> pubs = new List<Publication>();
            pubs = DBAdapter.GetPubs(res);
            

            pubs = sort_list(pubs);


            return pubs;
            }
       }

        public static List<Publication> FilterByYear(int year1, int year2, List<Publication> pubList)
        {
            // check first year lower
            int firstYear= Math.Min(year1, year2);
            int secondYear= Math.Max(year1, year2);
            List<Publication> filteredPubls = new List<Publication>();
            // Filter the publications based on the availability date range
            filteredPubls= pubList.Where(p =>
                p.AvailabilityDate.Year >= firstYear &&
                p.AvailabilityDate.Year <=secondYear 
            ).ToList();


            return filteredPubls;
        }
    }
}
