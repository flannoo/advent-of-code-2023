using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    public static class Mission1_1
    {
        public static void Run(string filepath)
        {
            var input = File.ReadAllLines(filepath);

            List<int> ints = new(); 

            foreach (var line in input)
            {
                int firstNumber = 0;
                int lastNumber = 0;

                bool first = false;
                for (int i = 0; i < line.Length; i++)
                {
                    if (char.IsDigit(line[i]))
                    {
                        if(!first)
                        {
                            firstNumber = int.Parse(line[i].ToString());
                            first = true;
                        }
                        else
                        {
                            lastNumber = int.Parse(line[i].ToString());
                        }
                    }
                }
                if(lastNumber == 0)
                    lastNumber = firstNumber;

                string result = $"{firstNumber}{lastNumber}";
                ints.Add(int.Parse(result));
            }

            int sum = ints.Sum();

            Console.WriteLine("Output Mission 1.1: " + sum);
        }
    }
}
