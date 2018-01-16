using AlaskaAir_CodeTest.Models;
using AlaskaAir_CodeTest.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlaskaAir_CodeTest;
using AlaskaAir_CodeTest.Controllers;
using System.Linq;

namespace AlaskaAir_CodeTest.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private Airports[] test_airports = new Airports[4];

        private Flights[] test_flights = new Flights[4]; 

        private string flights_file_path = @"C:\aws\beanstock\AlaskaAir-CodeTest\AlaskaAir-CodeTest.Tests\flights.csv";
        private string ap_file_path = @"C:\aws\beanstock\AlaskaAir-CodeTest\AlaskaAir-CodeTest.Tests\airports.csv";

        [TestInitialize]
        public void Init()
        {
            CSV test_file = new CSV();

            test_airports = test_file.LoadAirports(ap_file_path);

            Assert.IsNotNull(test_airports);
            Assert.AreEqual(4, test_airports.Length); 

            test_flights = test_file.LoadFlights(flights_file_path);

            Assert.IsNotNull(test_flights);
            Assert.AreEqual(48, test_flights.Length); 
        }

        [TestMethod]
        public void Suggestions()
        {
            Suggest sg = new Suggest();

            Select2Result[] results = sg.Airports("", test_airports); 

            // make sure airport suggestions return all values
            Assert.IsNotNull(results);

            Assert.AreEqual(4, results.Length);

            Suggest sg2 = new Suggest(); 

            Select2Result[] results_t2 = sg2.Airports("la", test_airports);

            Assert.AreEqual(2, results_t2.Length);

            Suggest sg3 = new Suggest();

            Select2Result[] results_t3 = sg3.Airports("sKY", test_airports);
             //check to see if airport names are being searched
            Assert.AreEqual(1, results_t3.Length);
        }

        [TestMethod]
        public void SortByOpp()
        {
            HomeController target = new HomeController();
            PrivateObject obj = new PrivateObject(target);

            // load arguments to pass to method.
            object[] args = new object[2] { test_flights, "maincabin_asc" };

            Flights[] sorted_arr = (Flights[])obj.Invoke("SortByOpp", args);

            Assert.IsNotNull(sorted_arr); 

            for (var i = 1; i < sorted_arr.Length; i++)
            {
                Assert.IsTrue((sorted_arr[i - 1].MainCabinPrice <= sorted_arr[i].MainCabinPrice));
            }

            // test asc order main cabin
            object[] args2 = new object[2] { test_flights, "maincabin_dsc" };

            Flights[] sorted_arr2 = (Flights[])obj.Invoke("SortByOpp", args2);

            Assert.IsNotNull(sorted_arr2);

            for (var i = 1; i < sorted_arr2.Length; i++)
            {
                Assert.IsTrue((sorted_arr2[i - 1].MainCabinPrice >= sorted_arr2[i].MainCabinPrice));
            }

            // test asc order first class
            object[] args3 = new object[2] { test_flights, "firstclass_asc" };

            Flights[] sorted_arr3 = (Flights[])obj.Invoke("SortByOpp", args3);

            Assert.IsNotNull(sorted_arr3);

            for (var i = 1; i < sorted_arr3.Length; i++)
            {
                Assert.IsTrue((sorted_arr3[i - 1].FirstClassPrice <= sorted_arr3[i].FirstClassPrice));
            }

            // test dsc order first class
            object[] args4 = new object[2] { test_flights, "firstclass_dsc" };

            Flights[] sorted_arr4 = (Flights[])obj.Invoke("SortByOpp", args4);

            Assert.IsNotNull(sorted_arr4);

            for (var i = 1; i < sorted_arr4.Length; i++)
            {
                Assert.IsTrue((sorted_arr4[i - 1].FirstClassPrice >= sorted_arr4[i].FirstClassPrice));
            }

            // test asc order departure time
            object[] args5 = new object[2] { test_flights, "departs_asc" };

            Flights[] sorted_arr5 = (Flights[])obj.Invoke("SortByOpp", args5);

            Assert.IsNotNull(sorted_arr5);

            for (var i = 1; i < sorted_arr5.Length; i++)
            {
                Assert.IsTrue((sorted_arr5[i - 1].Departs.Ticks <= sorted_arr5[i].Departs.Ticks));
            }

            // test dsc order departure time
            object[] args6 = new object[2] { test_flights, "departs_dsc" };

            Flights[] sorted_arr6 = (Flights[])obj.Invoke("SortByOpp", args6);

            Assert.IsNotNull(sorted_arr6);

            for (var i = 1; i < sorted_arr6.Length; i++)
            {
                Assert.IsTrue((sorted_arr6[i - 1].Departs.Ticks >= sorted_arr6[i].Departs.Ticks));
            }
        }

        [TestMethod]
        public void FlightByRoute()
        {
            Search s = new Search();

            // sort the array by code in ascending alphabetical order
            test_flights = test_flights.OrderBy(c => c.From).ToArray();

            var results = s.FlightsByRoute(test_flights, "SEA", "PHX");

            Assert.IsNotNull(results);

            Assert.AreEqual(4, results.Length);

            Assert.AreEqual("SEA", results[0].From);

            Assert.AreEqual("PHX", results[0].To);

            Assert.AreEqual("SEA", results[1].From);

            Assert.AreEqual("PHX", results[1].To);

            Assert.AreEqual("SEA", results[2].From);

            Assert.AreEqual("PHX", results[2].To);

            Assert.AreEqual("SEA", results[3].From);

            Assert.AreEqual("PHX", results[3].To);

        }
    }
}
