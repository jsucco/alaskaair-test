using AlaskaAir_CodeTest.Models; 
using AlaskaAir_CodeTest.Services; 
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlaskaAir_CodeTest;
using AlaskaAir_CodeTest.Controllers;

namespace AlaskaAir_CodeTest.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private Airports[] test_airports = new Airports[4]; 

        [TestInitialize]
        public void Init()
        {
            test_airports[0] = new Airports() { Code = "SEA", Name = "Seattle WA (SEA-Seattle/Tacoma Intl.)" };
            test_airports[1] = new Airports() { Code = "PHX", Name = "Phoenix AZ (PHX-Sky Harbor Intl.)" };
            test_airports[2] = new Airports() { Code = "LAS", Name = "Las Vegas NV (LAS-McCarran Intl.)" };
            test_airports[3] = new Airports() { Code = "LAX", Name = "Los Angeles CA (LAX-Los Angeles Intl.)" };
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
    }
}
