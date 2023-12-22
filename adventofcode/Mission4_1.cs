using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace adventofcode
{
    public static class Mission4_1
    {
        public static void Run(string filepath)
        {
            var input = File.ReadAllLines(filepath);

            List<ScratchCard> cards = new List<ScratchCard>();

            // Parse lines to scratchCard objects
            foreach (var line in input)
            {
                string name = line.Split(':')[0];
                string winningNumbers = line.Split(':')[1].Split('|')[0].Trim();
                string myNumbers = line.Split(':')[1].Split('|')[1].Trim();

                cards.Add(new ScratchCard()
                {
                    Name = name,
                    WinningNumbers = Regex.Split(winningNumbers, @"\s+").Select(x => int.Parse(x)).ToList(),
                    MyNumbers = Regex.Split(myNumbers, @"\s+").Select(x => int.Parse(x)).ToList(),
                });
            }

            int totalScore = 0;
            foreach (var card in cards)
            {
                int cardScore = 0;
                foreach (var number in card.WinningNumbers)
                {
                    if (card.MyNumbers.Contains(number))
                    {
                        if(cardScore == 0)
                            cardScore++;
                        else
                            cardScore *= 2;
                    }
                }
                totalScore += cardScore;
            }
            Console.WriteLine($"Output Mission 4.1: { totalScore}");
        }

    }

    internal class ScratchCard
    {
        public string Name { get; set; }
        public List<int> WinningNumbers { get; set; }
        public List<int> MyNumbers { get; set; }
    }
}
