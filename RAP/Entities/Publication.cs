using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_RAP.Entites
{
    internal class Publication
    {
        // Properties
        //public bool Q1Ranked { get; set; }
        //public DateTime PublicationYear { get; set; }
        public string Title { get; set; }
        public string DOI { get; set; }
        // is authors an actual list of researcher objects? or just a list of strings?
        //public List<Researcher> Authors { get; set; }
        public string Authors { get; set; }
        public string CiteAs { get; set; }
        public RankingType Ranking { get; set; }
        public DateTime AvailabilityDate { get; set; }
        //public int PageCount { get; set; }
        public PublicationType Type { get; set; }
        //public RankingType Ranking { get; set; }

        // Constructor
        public Publication( string title, string doi, string authors, string citeAs, DateTime availabilityDate, string type, string ranking)
        {
            //PublicationYear = publicationYear;
            Title = title;
            DOI = doi;
            Authors = authors;
            CiteAs = citeAs;
            AvailabilityDate = availabilityDate;
            //PageCount = pageCount;
            typeCalc(type);
            Ranking = (RankingType)Enum.Parse(typeof(RankingType), ranking);
            

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
        // Method to count cumulative publications
        //public int cumulativePubs()
        //{
        //    return Authors.Sum(author => author.Publications.Count);
        //}
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
