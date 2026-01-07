using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventToCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //example string for visualization
            string example = """
                .......S....... 1  .......S.......
                ............... 1  .......1.......
                .......^....... 2  ......1^1......
                ............... 2  ......1.1......
                ......^.^...... 4  .....1^2^1.....
                ............... 4  .....1.2.1.....
                .....^.^.^..... 8  ....1^3^3^1....
                ............... 8  ....1.3.3.1....
                ....^.^...^.... 13 ...1^4^331^1...
                ............... 13 ...1.4.331.1...
                ...^.^...^.^... 20 ..1^5^434^2^1..
                ............... 20 ..1.5.434.2.1..
                ..^...^.....^.. 26 .1^154^74.21^1.
                ............... 26 .1.154.74.21.1.
                .^.^.^.^.^...^. 40 .^.^.^.^.^...^.
                ............... 40 1 + 2 + 10 + 11 + 11 + 2 + 1 + 1 + 1 = 40                                   
                """;
            //example string
            example = """
                .......S.......
                ...............
                .......^.......
                ...............
                ......^.^......
                ...............
                .....^.^.^.....
                ...............
                ....^.^...^....
                ...............
                ...^.^...^.^...
                ...............
                ..^...^.....^..
                ...............
                .^.^.^.^.^...^.
                ...............
                """;

            //to use the example string enable this line and comment the line below
            //IEnumerable<string> array = example.Split("\r\n");
            IEnumerable<string> array = File.ReadLines(Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName, "input.txt"));

            //replace all . with 0, the start S with 1 and all ^ with -1 to create the diagramm out of integers
            List<List<long>> diagramm = array.Select(line => line.Replace('^', '2').Replace("S", "1").Replace(".", "0").ToList()).Select(line => line.Select(c => (long)char.GetNumericValue(c) is 2 ? -1 : (long)char.GetNumericValue(c)).ToList()).ToList();

            for (int row = 0; row < diagramm.Count - 1; row++)
            {
                for (int col = 0; col < diagramm[row].Count; col++)
                {
                    //get the current number and the number below it
                    long currentNum = diagramm[row][col];
                    long nextNum = diagramm[row + 1][col];

                    //if the current number is not a ^(-1) and not a .(0), then check if the long below is a splitter if it is split in two, else go straight down
                    if (currentNum is > 0)
                    {
                        if (nextNum is -1)
                        {
                            diagramm[row + 1][col + 1] += currentNum;
                            diagramm[row + 1][col - 1] += currentNum;
                        }
                        else
                        {
                            diagramm[row + 1][col] += currentNum;
                        }
                    }
                }
            }

            //the result is the sum of all the numbers in the last row
            long result = 0;
            foreach (long number in diagramm[diagramm.Count - 1])
            {
                result += number;
            }

            Console.WriteLine("One single beam has " + result + " different timelines!");
        }
    }
}