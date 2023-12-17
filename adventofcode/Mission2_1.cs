using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    public static class Mission2_1
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

            int redCubes = 12;
            int greenCubes = 13;
            int BlueCubes = 14;

            int result = 0;

            foreach (var game in games)
            {
                bool gamePossible = true;

                foreach (var set in game.Sets)
                {
                    if (set.Red > redCubes || set.Green > greenCubes || set.Blue > BlueCubes)
                    {
                        gamePossible = false;
                        break;
                    }
                }

                if (gamePossible)
                {
                    result += game.Id;
                }
            }

            Console.WriteLine("Output Mission 2.1: " + result);
        }
    }

    public class Set
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
    }

    public class Game
    {
        public int Id { get; set; }
        public List<Set> Sets { get; set; }
    }
}
