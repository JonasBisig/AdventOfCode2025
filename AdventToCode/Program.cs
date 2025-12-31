using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdventToCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //example string
            string example = "3-5,10-14,16-20,12-18,,1,5,8,11,17,32";

            //to use the example string enable this line and comment the line below
            //List<string> input = example.Split(",").ToList();
            List<string> input = File.ReadLines(Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName, "input.txt")).ToList();

            List<string> freshIDs = new List<string>();
            int test = 0;

            //fill the freshIDs list with all the fresh id ranges
            foreach (string ranges in input)
            {
                if (ranges.IsWhiteSpace())
                {
                    break;
                }
                else
                {
                    freshIDs.Add(ranges);
                }
            }

            //remove all the fresh id ranges
            input.RemoveRange(0, input.IndexOf("") + 1);

            //check for each id if the id is between a fresh id range
            int result = 0;
            foreach (string id in input)
            {
                foreach (string range in freshIDs)
                {
                    if (long.Parse(id) >= long.Parse(range.Split("-")[0]) && long.Parse(id) <= long.Parse(range.Split("-")[1]))
                    {
                        result++;
                        break;
                    }
                }                
            }
            
            Console.WriteLine(result + " of the available ingredient IDs are fresh!");
        }
    }
}