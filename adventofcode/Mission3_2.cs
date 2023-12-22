using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace adventofcode
{
    public static class Mission3_2
    {
        public static void Run(string filepath)
        {
            var input = File.ReadAllLines(filepath);

            List<PartNumber> partNumbers = new List<PartNumber>();
            List<PartCharacter> partCharacters = new List<PartCharacter>();

            int lineNumber = 1;

            // Create a list of all full numbers and their index position, line number and value
            // Also create list of all special characters (excluding ".") and their index position and line number
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
                        // Keep track of the special characters "*" and its index
                        if (character == '*')
                        {
                            partCharacters.Add(new PartCharacter() { Index = characterIndex, LineNumber = lineNumber, Value = character });
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

            // Keeps tracks of all numbers that have a star around it (left, right, up, down & diagonally) and are "attached" to another number
            List<int> gears = new List<int>();

            // for each * character check if there are multiple numbers around it
            foreach (var gear in partCharacters)
            {
                // Create index range to check for surrounding characters
                int gearStartIndex = gear.Index - 1;
                int gearEndIndex = gear.Index + 1;

                // Check if a number exists in the previous line in that index range
                var surroundedOnPreviousLine = new List<PartNumber>();
                foreach (var partNumber in partNumbers.Where(x => x.LineNumber == gear.LineNumber - 1))
                {
                    var startIndex = partNumber.Index;
                    var endIndex = partNumber.Index + partNumber.Value.ToString().Length - 1;

                    // if one of the numbers is in the range of the gear, add it to the list
                    if ((gearStartIndex >= startIndex && gearStartIndex <= endIndex) || (gearEndIndex >= startIndex && gearEndIndex <= endIndex) || (gearStartIndex <= startIndex && gearEndIndex >= endIndex))
                    {
                        surroundedOnPreviousLine.Add(partNumber);
                    }
                }

                // Check if a number exists in the current line in that index range
                var surroundedOnCurrentLine = new List<PartNumber>();
                foreach (var partNumber in partNumbers.Where(x => x.LineNumber == gear.LineNumber))
                {
                    var startIndex = partNumber.Index;
                    var endIndex = partNumber.Index + partNumber.Value.ToString().Length - 1;

                    // if the index of the character is in the range of the gear, add it to the list
                    if (startIndex == gearEndIndex || endIndex == gearStartIndex)
                    {
                        surroundedOnCurrentLine.Add(partNumber);
                    }
                }

                // Check if a number exists in the next line in that index range
                var surroundedOnNextLine = new List<PartNumber>();
                foreach (var partNumber in partNumbers.Where(x => x.LineNumber == gear.LineNumber + 1))
                {
                    var startIndex = partNumber.Index;
                    var endIndex = partNumber.Index + partNumber.Value.ToString().Length - 1;

                    // check if partnumber is in the range of gearStartIndex or gearEndIndex
                    if ((gearStartIndex >= startIndex && gearStartIndex <= endIndex) || (gearEndIndex >= startIndex && gearEndIndex <= endIndex) || (gearStartIndex <= startIndex && gearEndIndex >= endIndex))
                    {
                        surroundedOnPreviousLine.Add(partNumber);
                    }
                }

                var surrounded = (surroundedOnPreviousLine.Select(p => p.Value).Concat(surroundedOnCurrentLine.Select(p => p.Value)).Concat(surroundedOnNextLine.Select(p => p.Value)));

                // If there are exactly 2 numbers around the * character, multiply them and add them to the list of gears
                if (surrounded.Count() == 2)
                {
                    gears.Add(surrounded.Aggregate(1, (total, next) => total * next));
                }
            }


            int sum = gears.Sum();

            // Show result
            Console.WriteLine("Output Mission 3.2: " + sum);
        }
    }
}
