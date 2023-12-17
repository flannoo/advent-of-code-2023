using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    public static class Mission3_1
    {
        public static void Run(string filepath)
        {
            var input = File.ReadAllLines(filepath);

            List<PartNumber> partNumbers = new List<PartNumber>();
            List<PartCharacter> partCharacters = new List<PartCharacter>();

            int lineNumber = 1;
            foreach (var line in input)
            {
                // Keeps track of all full numbers in the line and the start index
                Dictionary<int, string> partNumber = new Dictionary<int, string>();

                char previousChar = '.';
                int previousIndex = 1;
                int characterIndex = 1;
                foreach (var character in line)
                {
                    // check if character is digit
                    if (char.IsDigit(character))
                    {
                        // check if previous character is digit
                        // if not, add the index and the character to the dictionary
                        if (!char.IsDigit(previousChar))
                        {
                            partNumber.Add(characterIndex, character.ToString());
                            previousIndex = characterIndex;
                        }
                        else
                        {
                            // if previous character is digit, add the character to the dictionary on that index
                            partNumber[previousIndex] += character.ToString();
                        }
                    }
                    else
                    {
                        // Keep track of the special characters (other than ".") and its index
                        if (character != '.')
                        {
                            partCharacters.Add(new PartCharacter() { Index = characterIndex, LineNumber = lineNumber });
                        }
                    }

                    previousChar = character;
                    characterIndex++;
                }

                // Add all numbers to the list including their index position, line number and value
                foreach (var item in partNumber)
                {
                    partNumbers.Add(new PartNumber() { Index = item.Key, LineNumber = lineNumber, Value = int.Parse(item.Value) });
                }

                lineNumber++;
            }

            // Keeps tracks of all numbers that have a symbol (other than ".") around it (left, right, up, down & diagonally)
            List<PartNumber> realPartNumbers = new List<PartNumber>();

            // Loop over each "special" character (other than ".") to find the surrounding numbers
            foreach (var partChar in partCharacters)
            {
                // check previous line, to find the up & diagonally numbers
                if (partChar.LineNumber > 1)
                {
                    // check previous line on the index
                    foreach (var previousPartNumber in partNumbers.Where(x => x.LineNumber == partChar.LineNumber - 1))
                    {
                        // if partchar index is in range of previousPartNumber startindex - 1 (for diagonally) and endindex (startindex + length + 1), then add it to realPartNumbers
                        if (partChar.Index >= (previousPartNumber.Index - 1) && partChar.Index <= (previousPartNumber.Index + previousPartNumber.Value.ToString().Length) + 1)
                        {
                            // if it's not already in the list, add it (avoid duplicates)
                            if (!realPartNumbers.Contains(previousPartNumber))
                                realPartNumbers.Add(previousPartNumber);
                        }
                    }
                }

                // check current line on the index, to find left and right numbers
                foreach (var currentPartNumber in partNumbers.Where(x => x.LineNumber == partChar.LineNumber))
                {
                    // if partchar index is in range of currentPartNumber startindex - 1 (left) and endindex (startindex + length + 1) (right), then add it to realPartNumbers
                    int endIndexRange = currentPartNumber.Index + currentPartNumber.Value.ToString().Length + 1;
                    if (partChar.Index >= currentPartNumber.Index - 1 && partChar.Index <= endIndexRange)
                    {
                        // if it's not already in the list, add it (avoid duplicates)
                        if (!realPartNumbers.Contains(currentPartNumber))
                            realPartNumbers.Add(currentPartNumber);
                    }
                }

                // check next line, to find the down & diagonally numbers
                if (partChar.LineNumber < partNumbers.Max(x => x.LineNumber))
                {
                    // check next line on the index
                    foreach (var nextPartNumber in partNumbers.Where(x => x.LineNumber == partChar.LineNumber + 1))
                    {
                        // if partchar index is in range of nextPartNumber startindex - 1 (for diagonally) and endindex (startindex + length + 1), then add it to realPartNumbers
                        if (partChar.Index >= (nextPartNumber.Index - 1) && partChar.Index <= (nextPartNumber.Index + nextPartNumber.Value.ToString().Length) + 1)
                        {
                            // if it's not already in the list, add it (avoid duplicates)
                            if (!realPartNumbers.Contains(nextPartNumber))
                                realPartNumbers.Add(nextPartNumber);
                        }
                    }
                }

            }

            int sum = 0;

            // Execute the summation of all numbers
            foreach (var item in realPartNumbers.OrderBy(x => x.LineNumber))
            {
                //Console.WriteLine("Line: " + item.LineNumber + " - value: " + item.Value);
                sum += item.Value;
            } 

            // Show result
            Console.WriteLine("Output Mission 3.1: " + sum);
        }
    }

    internal class PartCharacter
    {
        public int Index { get; set; }
        public int LineNumber { get; set; }
    }

    internal class PartNumber
    {
        public int Index { get; set; }
        public int LineNumber { get; set; }
        public int Value { get; set; }
    }
}
