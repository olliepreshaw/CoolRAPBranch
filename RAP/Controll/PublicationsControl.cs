using KIT206_RAP.DataBase;
using KIT206_RAP.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Controll
{
    internal class PublicationsControl
    {
        public static List<Publication> FetchPublications(Researcher res)
        {
            List <Publication> pubs = new List<Publication>();
            pubs = DBAdapter.GetPubs(res);

            return pubs;
        }
    }
}
