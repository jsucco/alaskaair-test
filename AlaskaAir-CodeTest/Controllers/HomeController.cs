using System.Web;
using System.Web.Mvc;
using AlaskaAir_CodeTest.Models;
using AlaskaAir_CodeTest.Services;
using System;

namespace AlaskaAir_CodeTest.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(Duration = 5356800, VaryByParam = "none")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AirportSearch(string searchTerm, int pageSize, int pageNum)
        {
            var ca_o = HttpRuntime.Cache["airports-data"];
            var sel_o = new Select2Result[] { new Select2Result { id = "", text = "none" } };

            Suggest sg = new Suggest();

            Select2PagedResult aps_o = new Select2PagedResult
            {
                Total = 1,
                Results = sel_o
            };
            try
            {
                if (ca_o != null)
                {
                    Airports[] all_airports = (Airports[])ca_o;

                    aps_o.Results = sg.Airports(searchTerm, all_airports);
                }
            } catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
            }
            

            return new JsonResult
            {
                Data = aps_o,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public ActionResult GetFlights(string airport1, string airport2)
        {
            var flights = (Flights[])HttpRuntime.Cache["flights-data"];
            Flights[] results = new Flights[] { }; 

            //check if data is in cache.  global.asax file should check cache and load if not present.
            if (flights != null)
            {
                try
                {
                    Search s = new Search();

                    results = s.FlightsByRoute(flights, airport1, airport2);

                    string sessionId = HttpContext.Session.SessionID;

                    CacheContextResults(sessionId, results); 

                } catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
                }
                
            }

            return new JsonResult
            {
                Data = results,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public ActionResult SortFlights(string oper)
        {
            
            Flights[] results = new Flights[] { }; 

            try
            {
                var sessionId = HttpContext.Session.SessionID;
                var cache_src = HttpRuntime.Cache["user-results-" + sessionId];

                if (cache_src != null)
                {
                    Flights[] src = (Flights[])cache_src;

                    results = SortByOpp(src, oper); 
                }
                

            } catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
            }

            return new JsonResult
            {
                Data = results,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #region helpers

        private void CacheContextResults(string SessionId, Flights[] results) 
        {
            if (results.Length > 0) {
                HttpRuntime.Cache.Insert("user-results-" + SessionId,
                    results,
                    null,
                    DateTime.Now.AddMonths(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                ); 
            }

        }

        private Flights[] SortByOpp(Flights[] fs,string oper)
        {
            Flights[] sorted = new Flights[] { }; 
            if (fs.Length > 0)
            {
                switch (oper)
                {
                    case "maincabin_asc":
                        sorted = Services.SortFlights.CabinAsc(fs); 
                        break;
                    case "maincabin_dsc":
                        sorted = Services.SortFlights.CabinDsc(fs); 
                        break;
                    case "firstclass_asc":
                        sorted = Services.SortFlights.FirstClassAsc(fs);
                        break;
                    case "firstclass_dsc":
                        sorted = Services.SortFlights.FirstClassDsc(fs);
                        break;
                    case "departs_asc":
                        sorted = Services.SortFlights.DepartAsc(fs);
                        break;
                    case "departs_dsc":
                        sorted = Services.SortFlights.DepartDsc(fs);
                        break;
                    default:
                        sorted = fs;
                        break;                 
                }
            }
            return sorted; 
        }


        #endregion
    }
}