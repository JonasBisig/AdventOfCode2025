using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdventToCode
{
    class State
    {
        public List<int> Value { get; set; }  // Aktueller Zustand
        public int Steps { get; set; }   // Anzahl Knopfdrücke
        public List<int> Path { get; set; }  // Welche Knöpfe gedrückt wurden
    }

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
            IEnumerable<string> array = example.Split("\r\n");
            //IEnumerable<string> array = File.ReadLines(Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName, "input.txt"));
            int machineCount = 0;
            int result = 0;
            foreach (string line in array)
            {
                machineCount++;
                var actions = line.Split(" ");
                List<List<int>> buttons = new List<List<int>>();
                List<int> joltage = new List<int>();
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

                Queue<State> queue = new Queue<State>();
                HashSet<List<int>> visited = new HashSet<List<int>>();

                List<int> startValue = joltage.Select(i => 0).ToList();
                queue.Enqueue(new State { Value = startValue.ToList(), Steps = 0, Path = new List<int>() });
                visited.Add(startValue.ToList());

                int minSteps = 0;
                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();

                    if (current.Steps >= 5)
                    {

                    }

                    if (current.Value == joltage)
                    {
                        minSteps = current.Steps;
                        break;
                    }

                    foreach (var btn in buttons)
                    {
                        List<int> newValue = ToggleJoltage(current.Value, btn).ToList();

                        bool joltageToHigh = false;
                        for(int i = 0; i < newValue.Count; i++)
                        {
                            if (newValue[i] > joltage[i])
                                joltageToHigh = true;
                        }

                        if (!joltageToHigh)
                        {
                            visited.Add(newValue.ToList());
                            var newPath = new List<int>(current.Path) { buttons.IndexOf(btn) };
                            queue.Enqueue(new State
                            {
                                Value = newValue.ToList(),
                                Steps = current.Steps + 1,
                                Path = newPath
                            });
                        }
                    }
                }

                result += minSteps;
                Console.WriteLine($"In the {machineCount}th machine the buttons must be pressed {minSteps} times.");
            }

            Console.WriteLine($"In total, the buttons must be pressed {result} times.");
        }

        private static string ToggleLamps(string lamps, List<int> button)
        {
            StringBuilder sb = new StringBuilder(lamps);
            foreach (int pos in button)
            {
                if (sb[pos+1] == '.')
                {
                    sb[pos+1] = '#';
                }
                else
                {
                    sb[pos+1] = '.';
                }
            }
            return sb.ToString();
        }

        private static List<int> ToggleJoltage(List<int> joltage, List<int> button)
        {
            foreach (int pos in button)
            {
                joltage[pos] += 1;
            }
            return joltage;
        }
    }
}