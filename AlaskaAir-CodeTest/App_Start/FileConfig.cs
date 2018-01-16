using System;
using System.Web;
using System.Linq; 
using AlaskaAir_CodeTest.Services; 

namespace AlaskaAir_CodeTest
{
    public partial class FileConfig
    {
        private static string BasePath = HttpContext.Current.Server.MapPath("~");

        public static void CacheAppData()
        {
            cacheFlights();
            cacheAirports(); 
        }

        private static void cacheFlights()
        {
            CSV test_data = new CSV();

            var f_p = BasePath + "flights.csv";

            var flights = test_data.LoadFlights(f_p);

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
            CSV test_data = new CSV();

            var a_p = BasePath + "airports.csv";

            var airports = test_data.LoadAirports(a_p);

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