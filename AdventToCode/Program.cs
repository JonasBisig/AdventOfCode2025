using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace AdventToCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //example string
            string example = "..@@.@@@@.,@@@.@.@.@@,@@@@@.@.@@,@.@@@@..@.,@@.@@@@.@@,.@@@@@@@.@,.@.@.@.@@@,@.@@@.@@@@,.@@@@@@@@.,@.@.@@@.@.";

            //to use the example string enable this line and comment the line below
            //IEnumerable<string> array = example.Split(",");
            IEnumerable<string> array = File.ReadLines(Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName, "input.txt"));

            int result = 0;

            List<List<char>> paperRack = new List<List<char>>();

            //build the array
            foreach (string r in array)
            {
                paperRack.Add(r.ToList());
            }

            int rowOffset= -1;
            int colOffset = -1;

            for (int curRow = 0; curRow < paperRack.Count; curRow++)
            {
                for (int curCol = 0; curCol < paperRack[curRow].Count; curCol++)
                {
                    char curItem = paperRack[curRow][curCol];
                    int rollCount = 0;
                    while (true)
                    {
                        if (curItem != '@') break;

                        //when the offset is pointing at the curItem don't increment rollCount
                        if (rowOffset != 0 || colOffset != 0)
                        {
                            //check if index curRow + rowOffset is out of bounds
                            if (curRow + rowOffset >= 0 && curRow + rowOffset < paperRack.Count)
                            {
                                //check if index curCol + colOffset is out of bounds
                                if (curCol + colOffset >= 0 && curCol + colOffset < paperRack[curRow + rowOffset].Count)
                                {
                                    //if the offset position is a paper roll, increment rollCount
                                    if (paperRack[curRow + rowOffset][curCol + colOffset] == '@')
                                    {
                                        rollCount++;
                                    }
                                }
                            }
                        }

                        //move the offsets  ->  -1, -1  ->  0, -1  ->  1, -1  ->  -1, 0  ->  0, 0  ->  1, 0  ->  -1, 1  ->  0, 1  ->  1, 1
                        if (colOffset == 1)
                        {
                            colOffset = -1;
                            if (rowOffset == 1)
                            {
                                rowOffset = -1;
                                break;
                            }
                            else
                            {
                                rowOffset++;
                            }
                        }
                        else
                        {
                            colOffset++;
                        }
                    }

                    //check if the curItem is a paper roll and if there are less then 4 adjacent paper rolls
                    if (curItem == '@' && rollCount < 4)
                    {
                        result++;
                    }
                }
            }

            Console.WriteLine("There are " + result + " paper rolls that can be moved!");
        }
    }
}