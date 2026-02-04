using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdventToCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //example string
            string example = """
                [.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
                [...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
                [.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}
                """;
            //to use the example string enable this line and comment the line below
            //IEnumerable<string> array = example.Split("\r\n");
            IEnumerable<string> array = File.ReadLines(Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName, "input.txt"));

            int machineCount = 0;
            int result = 0;
            foreach (string line in array)
            {
                machineCount++;
                var actions = line.Split(" ");
                List<List<int>> buttons = new List<List<int>>();
                List<int> joltage = null;
                foreach (string action in actions)
                {
                    if (action.StartsWith("("))
                    {
                        buttons.Add(action.Substring(1, action.Length - 2).Split(",").Select(int.Parse).ToList());
                    }
                    else if (action.StartsWith("{"))
                    {
                        joltage = action.Substring(1, action.Length - 2).Split(",").Select(int.Parse).ToList();
                    }
                }

                //int dimensions = joltage.Count;
                List<int> current = joltage.Select(i=>0).ToList();

                // Buttons nach "Wirkung" sortieren (starkes Pruning!)
                buttons = buttons.OrderByDescending(b => b.Count).ToList();

                int best = int.MaxValue;

                DFS(0, buttons, joltage, current, 0, ref best, machineCount);

                result += best;
                Console.WriteLine($"In the {machineCount}th machine the buttons must be pressed {best} times.");
            }

            Console.WriteLine($"In total, the buttons must be pressed {result} times.");
        }

        private static List<int> ToggleJoltage(string joltage, List<int> button)
        {
            List<int> newJoltage = joltage.Split(",").Select(int.Parse).ToList();
            foreach (int pos in button)
            {
                newJoltage[pos] += 1;
            }
            return newJoltage;
        }

        static void DFS(int buttonIndex, List<List<int>> buttons, List<int> target, List<int> current, int steps, ref int best, int machineCount)
        {
            if (steps >= best)
                return;

            if (IsTarget(current, target))
            {
                best = steps;
                return;
            }

            if (buttonIndex >= buttons.Count)
                return;

            List<int> button = buttons[buttonIndex];

            // Maximal mögliche Drücke dieses Buttons
            int maxPresses = int.MaxValue;
            foreach (int pos in button)
            {
                if (current[pos] >= target[pos])
                    maxPresses = 0;
                else
                    maxPresses = Math.Min(
                        maxPresses,
                        target[pos] - current[pos]
                    );
            }

            // Wichtig: von groß nach klein → schneller gute Lösungen
            for (int k = maxPresses; k >= 0; k--)
            {
                Apply(button, current, k);
                DFS(buttonIndex + 1, buttons, target, current, steps + k, ref best, machineCount);
                Apply(button, current, -k); // Undo
            }
        }


        static void Apply(List<int> button, List<int> current, int times)
        {
            foreach (int pos in button)
                current[pos] += times;
        }

        static bool IsTarget(List<int> current, List<int> target)
        {
            for (int i = 0; i < current.Count; i++)
                if (current[i] != target[i])
                    return false;
            return true;
        }
    }
}