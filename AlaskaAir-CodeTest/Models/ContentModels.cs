using System; 

namespace AlaskaAir_CodeTest.Models
{
    public class Flights
    {
        public string From { get; set; }
        public string To { get; set; }
        public int FlightNumber { get; set; }
        public DateTime Departs { get; set; }
        public DateTime Arrives { get; set; }
        public string DepartStr { get; set; }
        public string ArriveStr { get; set; }
        public decimal MainCabinPrice { get; set; }
        public decimal FirstClassPrice { get; set; }
    }

    public class Airports
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}