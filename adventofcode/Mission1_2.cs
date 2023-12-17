using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    public static class Mission1_2
    {
        public static void Run(string filepath)
        {
            var input = File.ReadAllLines(filepath);

            Dictionary<string, int> stringNumbers = new();
            stringNumbers.Add("one", 1);
            stringNumbers.Add("two", 2);
            stringNumbers.Add("three", 3);
            stringNumbers.Add("four", 4);
            stringNumbers.Add("five", 5);
            stringNumbers.Add("six", 6);
            stringNumbers.Add("seven", 7);
            stringNumbers.Add("eight", 8);
            stringNumbers.Add("nine", 9);

            List<int> ints = new();

            int count = 1;
            foreach (var line in input)
            {                
                string wordToCheck = "";
                string newLine = "";

                for (int i = 0; i < line.Length; i++)
                {
                    
                    if (char.IsLetter(line[i]))
                    {
                        wordToCheck += line[i];

                        foreach (var stringNumber in stringNumbers)
                        {
                            if (wordToCheck.Contains(stringNumber.Key))
                            {
                                newLine += wordToCheck.Replace(wordToCheck, stringNumber.Value.ToString());

                                // Keep last 2 chars after replace to avoid overlap of numbers (eg. "oneight" should result in "one" and "eight")
                                // So once "one" is replaced, We remove all characters from "wordToCheck" except the last 2

                                // example for the line "vdoneightsix7h9"
                                // "vdone" will first be detected. We replace it with "1" but keep "ne" in "wordToCheck",
                                // so next iteration the check will become "neight", which still includes "eight"
                                var stringNumberRemoved = wordToCheck.Remove(wordToCheck.Length - 2);
                                wordToCheck = wordToCheck.Replace(stringNumberRemoved, "");

                                break;
                            }
                        }
                    }
                    else
                    {
                        if (wordToCheck != "")
                        {
                            foreach (var stringNumber in stringNumbers)
                            {
                                if (wordToCheck.Contains(stringNumber.Key))
                                {
                                    newLine += newLine.Replace(wordToCheck, stringNumber.Value.ToString());
                                    break;
                                }
                            }
                        }
                        wordToCheck = "";
                        newLine += line[i];
                    }
                }

                int firstNumber = 0;
                int lastNumber = 0;

                bool first = false;
                for (int i = 0; i < newLine.Length; i++)
                {
                    if (char.IsDigit(newLine[i]))
                    {
                        if (!first)
                        {
                            firstNumber = int.Parse(newLine[i].ToString());
                            first = true;
                        }
                        else
                        {
                            lastNumber = int.Parse(newLine[i].ToString());
                        }
                    }
                }

                if (lastNumber == 0)
                    lastNumber = firstNumber;

                string result = $"{firstNumber}{lastNumber}";
                
                //Console.WriteLine(count.ToString() + ": " + result);
                ints.Add(int.Parse(result));

                count++;
            }

            int sum = 0;
            foreach (var i in ints)
            {
                sum += i;
            }

            Console.WriteLine("Output Mission 1.2: " + sum);
        }
    }
}
