using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_RAP.Entites
{
    public enum Level
    { 
        Student,
        A, 
        B, 
        C, 
        D, 
        E 
    }
 
    internal class Position
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Level Level{ get; set; }
        
        
        public Position(DateTime startDate, DateTime? endDate, string level)
        {
            StartDate = startDate;
            EndDate = endDate;
            Level = CalcPosLevel(level);
        }
         public Level CalcPosLevel(string lev)
        {
            if (lev.Equals("A"))
            {
                return Level.A;
            }else if (lev.Equals("B"))
            {
                return Level.B;
            }else if (lev.Equals("C"))
            {
                return Level.C;
            }
            return Level.D;
        }      
    }
}
