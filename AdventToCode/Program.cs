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

            List<List<char>> diagramm = array.Select(line => line.ToList()).ToList();
            long result = 0;

            //go through each row except the last one
            for (int row = 0; row < diagramm.Count - 1; row++)
            {
                for (int col = 0; col < diagramm[row].Count; col++)
                {
                    //get the current char and the char below it
                    char currentChar = diagramm[row][col];
                    char nextChar = diagramm[row+1][col];
                    
                    //if the current char is the start S or the Beam, then check if the beam has to split or not
                    if (currentChar is 'S' or '|')
                    {
                        //split the beam if the next char is a splitter ^
                        if (nextChar is '^')
                        {
                            diagramm[row + 1][col + 1] = '|';
                            diagramm[row + 1][col - 1] = '|';
                            result++;
                        }
                        else
                        {
                            diagramm[row + 1][col] = '|';
                        }
                    }
                }
            }
            //Display the final diagramm
            Console.WriteLine(string.Join("\n", diagramm.Select(row => new string(row.ToArray()))));
            Console.WriteLine("The beam has split "+ result + " times!");
        }
    }
}