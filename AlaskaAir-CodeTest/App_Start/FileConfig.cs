using System;
using System.Web;
using System.Linq; 
using AlaskaAir_CodeTest.Services; 

namespace AlaskaAir_CodeTest
{
    public partial class FileConfig : CSV
    {
        public static void CacheAppData()
        {
            cacheFlights();
            cacheAirports(); 
        }

        private static void cacheFlights()
        {
            var flights = LoadFlights();

            if (flights.Length == 0)
                throw new Exception("Failed to load flights csv file");

            // sort the array by code in ascending alphabetical order
            flights = flights.OrderBy(c => c.From).ToArray();

            HttpRuntime.Cache.Insert("flights-data",
                flights,
                null,
                DateTime.Now.AddMonths(1),
                System.Web.Caching.Cache.NoSlidingExpiration
            );
        }

        private static void cacheAirports()
        {
            var airports = LoadAirports();

            if (airports.Length == 0)
                throw new Exception("Failed to load airports csv file");

            HttpRuntime.Cache.Insert("airports-data",
                airports,
                null,
                DateTime.Now.AddMonths(1),
                System.Web.Caching.Cache.NoSlidingExpiration
            );
        }
    }
}