using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    public static class Mission2_2
    {
        public static void Run(string filepath)
        {
            var input = File.ReadAllLines(filepath);

            List<Game> games = new List<Game>();

            foreach (var line in input)
            {
                var fragments = line.Split(':');
                string id = fragments[0][5..];

                var game = new Game();
                game.Id = int.Parse(id);
                game.Sets = new List<Set>();

                var sets = fragments[1].Split(';');
                foreach (var set in sets)
                {
                    var setFragments = set.Split(',');
                    int red = 0;
                    int green = 0;
                    int blue = 0;

                    foreach (var setFragment in setFragments)
                    {
                        if (setFragment.Contains("red"))
                        {
                            red = int.Parse(setFragment.Split(' ')[1]);
                        }
                        else if (setFragment.Contains("green"))
                        {
                            green = int.Parse(setFragment.Split(' ')[1]);
                        }
                        else if (setFragment.Contains("blue"))
                        {
                            blue = int.Parse(setFragment.Split(' ')[1]);
                        }
                    }

                    game.Sets.Add(new Set()
                    {
                        Blue = blue,
                        Green = green,
                        Red = red
                    });
                }

                games.Add(game);
            }

            

            int result = 0;

            foreach (var game in games)
            {
                int minimumRedCubes = 0;
                int minimumGreenCubes = 0;
                int minimumBlueCubes = 0;

                foreach (var set in game.Sets)
                {
                    if(set.Red > minimumRedCubes)
                    {
                        minimumRedCubes = set.Red;
                    }
                    if (set.Green > minimumGreenCubes)
                    {
                        minimumGreenCubes = set.Green;
                    }
                    if (set.Blue > minimumBlueCubes)
                    {
                        minimumBlueCubes = set.Blue;
                    }
                }

                int gameResult = minimumBlueCubes * minimumGreenCubes * minimumRedCubes;

                result += gameResult;
            }

            Console.WriteLine("Output Mission 2.2: " + result);
        }
    }
}
