using System;
using System.Web;
using AlaskaAir_CodeTest.Models;
using System.Collections.Concurrent; 
using System.Threading.Tasks;  

namespace AlaskaAir_CodeTest.Services
{
    public class Suggest
    {
        ConcurrentBag<Select2Result> found = new ConcurrentBag<Select2Result>();
        ConcurrentDictionary<string, bool> dict = new ConcurrentDictionary<string, bool>(); 
      
        Airports[] all_airports;

        public Select2Result[] Airports(string str, Airports[] src)
        {

            all_airports = src; 

            Task[] tasks = new Task[2]; 

            //search codes and names for search string in two different tasks
            tasks[0] = Task.Factory.StartNew(() => searchCodes(str));
            tasks[1] = Task.Factory.StartNew(() => searchNames(str));

            Task.WaitAll(tasks);

            return found.ToArray(); 
        }

        private void searchCodes(string str)
        {
            str = str.Trim().ToUpper(); 

            for (int i = 0; i < all_airports.Length; i++)
            {
                string c = all_airports[i].Code.ToUpper(); 

                //dict keeps track of airports that have already been found to contains search string
                if (c.Contains(str) && !dict.ContainsKey(all_airports[i].Code))
                {
                    var added = dict.TryAdd(all_airports[i].Code, true);

                    if (added)
                    {
                        found.Add(new Select2Result()
                        {
                            id = all_airports[i].Code,
                            text = "(" + all_airports[i].Code + ") " + all_airports[i].Name
                        });
                    }
                                     
                }
                            
            }

        }

        private void searchNames(string str)
        {
            str = str.Trim().ToUpper(); 

            for (int i = 0; i < all_airports.Length; i++)
            {
                string n = all_airports[i].Name.ToUpper();

                if (n.Contains(str) && !dict.ContainsKey(all_airports[i].Code))
                {
                    var added = dict.TryAdd(all_airports[i].Code, true);

                    if (added)
                    {
                        found.Add(new Select2Result()
                        {
                            id = all_airports[i].Code,
                            text = "(" + all_airports[i].Code + ") " + all_airports[i].Name
                        });
                    }
                                       
                }
                    
            } 
        }
    }
}