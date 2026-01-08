using System;
using System.Collections.Generic;
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
            string example = """
                7,1                
                11,1
                11,7
                9,7
                9,5
                2,5
                2,3
                7,3
                """;

            //to use the example string enable this line and comment the line below
            IEnumerable<string> array = example.Split("\r\n");
            //IEnumerable<string> array = File.ReadLines(Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName, "input.txt"));

            List<Tuple<int, int>> pos = array.Select(x => (int.Parse(x.Split(",")[0]), int.Parse(x.Split(",")[1])).ToTuple()).ToList();

            List<List<Tuple<int, int>>> positions = array.Select((a, i) => array.Skip(i + 1).Select(b => new List<Tuple<int, int>>([(1,1).ToTuple()])).ToList()).ToList();
        }
    }
}