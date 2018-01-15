using System.Collections.Generic;
using AlaskaAir_CodeTest.Models; 

namespace AlaskaAir_CodeTest.Services
{
    public class Search
    {
        private Flights[] flights; 

        public Flights[] FlightsByRoute(Flights[] src, string from, string to)
        {
            List<Flights> results = new List<Flights>();

            // src arry empty nothing to find
            if (src == null || src.Length == 0)
                results.ToArray();

            flights = src; 

            // binary search to find first instance of from airport from left to right
            int fr_index = FlightsBinarySearch(from); 

            if (fr_index >= 0)
            {
                //collect matching route parameters
                for (var i = fr_index; i < flights.Length; i++)
                {
                    if (flights[i].From != from)
                        break;

                    if (flights[i].To == to)
                        results.Add(flights[i]); 
                }
            }

            return results.ToArray(); 
        } 

        public int FlightsBinarySearch(string airport)
        {
            int low = 0;
            int high = flights.Length - 1;
            int middle = (low + high + 1) / 2; // find the middle element
            int index = -1; // returns -1 if not found

            if (flights[middle].From == airport && low == high)
                return middle; 
            
            while (low < high)
            {
                if (airport == flights[middle].From && airport != flights[middle - 1].From)
                {
                    return middle;
                }
                else if (airport == flights[middle].From && middle == 1)
                {
                    return 0; 
                }
                else if (string.Compare(airport, flights[middle].From) == -1)
                {
                    high = middle - 1;
                }
                else if (string.Compare(airport, flights[middle].From) == 1)
                {
                    low = middle + 1;
                }
                else
                    high = middle - 1;

                middle = (low + high + 1) / 2; // recalculate the middle index
            }

            return index; 
        }
    }

    

}