
namespace AdventToCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //example string
            string example = "L68,L30,R48,L5,R60,L55,L1,L99,R14,L82";

            //Read Input File
            //to use the example string enable this line and comment the line below
            //IEnumerable<string> inputLines = example.Split(",");
            IEnumerable<string> inputLines = File.ReadLines(Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName, "input.txt"));

            //Dial starts at 50
            int dial = 50;
            int password = 0;
            foreach (string line in inputLines)
            {
                //Extract Direction -> R = +, L = -
                string direction = line.Substring(0, 1);

                //Extract the click count
                int clicks = int.Parse(line.Substring(1, line.Length - 1));

                //Add or Subtract the clicks from the dial
                if (direction == "R")
                {
                    //Make a for loop for clicks many times and increment dial each time
                    for (int i = 0; i < clicks; i++)
                    {
                        dial++;
                        //reset dial if it overflows and increment password
                        if (dial == 100)
                        {
                            dial = 0;
                            password++;
                        }
                    }                    
                }
                else
                {
                    //Make a for loop for clicks many times and decrement dial each time
                    for (int i = 0; i < clicks; i++)
                    {
                        dial--;
                        //if dial is at 0 increment password and if its overflowing reset dial to 99
                        if (dial == 00)
                        {
                            password++;
                        }
                        else if (dial == -1)
                        {
                            dial = 99;
                        }
                    }
                }
            }

            //Display the final Password
            Console.WriteLine(password);
        }
    }
}