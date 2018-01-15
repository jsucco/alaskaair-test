using System.Linq;
using AlaskaAir_CodeTest.Models; 

namespace AlaskaAir_CodeTest.Services
{
    public class SortFlights
    {

        public static Flights[] CabinAsc(Flights[] src)
        {
            return src.OrderBy(c => c.MainCabinPrice).ToArray(); 
        }

        public static Flights[] CabinDsc(Flights[] src)
        {
            return src.OrderByDescending(c => c.MainCabinPrice).ToArray(); 
        }

        public static Flights[] FirstClassAsc(Flights[] src)
        {
            return src.OrderBy(c => c.FirstClassPrice).ToArray(); 
        }

        public static Flights[] FirstClassDsc(Flights[] src)
        {
            return src.OrderByDescending(c => c.FirstClassPrice).ToArray(); 
        }

        public static Flights[] DepartAsc(Flights[] src)
        {
            return src.OrderBy(c => c.Departs.TimeOfDay).ToArray(); 
        }

        public static Flights[] DepartDsc(Flights[] src)
        {
            return src.OrderByDescending(c => c.Departs.TimeOfDay).ToArray(); 
        }
    }
}