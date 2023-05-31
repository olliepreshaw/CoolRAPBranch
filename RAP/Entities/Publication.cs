using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_RAP.Entites
{
    public class Publication
    {
        public string Title { get; set; }
        public string DOI { get; set; }
        public string Authors { get; set; }
        public string CiteAs { get; set; }
        public RankingType Ranking { get; set; }
        public DateTime AvailabilityDate { get; set; }
        public PublicationType Type { get; set; }
        public int Age { get; set; }

        // Constructor
        public Publication( string title, string doi, string authors, string citeAs, DateTime availabilityDate, string type, string ranking)
        {
            Title = title;
            DOI = doi;
            Authors = authors;
            CiteAs = citeAs;
            AvailabilityDate = availabilityDate;
            typeCalc(type);
            Ranking = (RankingType)Enum.Parse(typeof(RankingType), ranking);
            Age = Convert.ToInt32((DateTime.Today - AvailabilityDate).TotalDays);
        }

        public void typeCalc(string st)
        {
            if (st.Equals("Conference"))
            {
                Type = PublicationType.Conference;
            }
            else
            {
                Type = PublicationType.Journal;
            }

        }

    }
    

    public enum PublicationType
    {
        Conference,
        Journal
    }

    // can not find in db
    public enum RankingType
    {
        Q1,
        Q2,
        Q3,
        Q4
    }
 
}
