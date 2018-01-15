using System;
using System.Collections.Generic;
using System.IO; 
using System.Web;
using AlaskaAir_CodeTest.Models; 

namespace AlaskaAir_CodeTest.Services
{
    public class CSV : Parser
    {
        public static List<string> ErrorList = new List<string>();
        public static bool ErrorFlag = false;

        private static string BasePath = HttpContext.Current.Server.MapPath("~");

        protected static Flights[] LoadFlights()
        {
            List<Flights> list = new List<Flights>(); 

            var f_p = BasePath + "flights.csv";

            if (File.Exists(f_p) == false)
                throw new Exception("flights.csv file required at root of directory");                 

            using (StreamReader sr = new StreamReader(f_p))
            {
                //skip the first line consisting of header info
                sr.ReadLine(); 

                while (sr.Peek() >= 0)
                {
                    try
                    {
                        var f_o = processFlight(sr.ReadLine());

                        if (f_o != null)
                            list.Add(f_o); 

                    } catch (Exception ex)
                    {
                        ErrorList.Add(ex.Message);
                        ErrorFlag = true; 
                    }                        
                }
            }
            
            return list.ToArray(); 
        }

        private static Flights processFlight(string line)
        {
            line = line.Trim(); 

            if (line.Length > 0)
            {
                string[] col_data = line.Split(',');

                if (col_data.Length != 7)
                    throw new Exception("flights file column mismatch");

                Flights f_o = new Flights()
                {
                    From = col_data[0],
                    To = col_data[1],
                    FlightNumber = ToInt32(col_data[2]),
                    Departs = ToDateTime(col_data[3]),
                    Arrives = ToDateTime(col_data[4]),
                    DepartStr = ToDateTime(col_data[3]).ToString("t"),
                    ArriveStr = ToDateTime(col_data[4]).ToString("t"),
                    MainCabinPrice = ToDecimal(col_data[5]), 
                    FirstClassPrice = ToDecimal(col_data[6]),
                };

                if (ParseErrorFlag)
                    ErrorList.Add(ParseErrorMessage);

                return f_o;
            }

            return null; 
        }

        protected static Airports[] LoadAirports()
        {
            List<Airports> list = new List<Airports>();

            var a_p = BasePath + "airports.csv";

            if (File.Exists(a_p) == false)
                throw new Exception("airports.csv file required at root of directory");

            using (StreamReader sr = new StreamReader(a_p))
            {
                //skip the first line consisting of header info
                sr.ReadLine();

                while (sr.Peek() >= 0)
                {
                    try
                    {
                        var a_o = processAirport(sr.ReadLine());

                        if (a_o != null)
                            list.Add(a_o);

                    }
                    catch (Exception ex)
                    {
                        ErrorList.Add(ex.Message);
                        ErrorFlag = true;
                    }
                }
            }

            return list.ToArray();
        }

        private static Airports processAirport(string line)
        {
            line = line.Trim(); 

            if (line.Length > 0)
            {
                string[] col_data = line.Split(',');

                if (col_data.Length != 2)
                    throw new Exception("airports file column mismatch");

                Airports a_o = new Airports()
                {
                    Code = col_data[0],
                    Name = col_data[1],
                };

                if (ParseErrorFlag)
                    ErrorList.Add(ParseErrorMessage);

                return a_o; 
            }

            return null; 
        }
    }
}