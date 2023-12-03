using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2023.Day1
{
    internal static class Main
    {
        internal static async Task Run()
        {
            var f = await File.ReadAllLinesAsync("Day1/input.txt");
            var r = new Regex("\\d");
            //var res1 = f.Select(a=>int.Parse(r.Matches(a).First().Value + r.Matches(a).Last().Value));
            //Console.WriteLine($"Part 1: {res1.Sum()}");
            var res2 = f.Select(a => int.Parse(r.Matches(Rep(a)).First().Value + r.Matches(Rep(a)).Last().Value));
            Console.WriteLine($"Part 2: {res2.Sum()}");
        }

        private static List<(string, int)> _s_words = new List<(string, int)>()
            {
                ("zero", 0),
                ("one", 1),
                ("two", 2),
                ("three", 3),
                ("four", 4),
                ("five", 5),
                ("six", 6),
                ("seven", 7),
                ("eight", 8),
                ("nine", 9)
            };

        private static string Rep(string a)
        {
            _s_words.ForEach(_s_words => a = a.Replace(_s_words.Item1, _s_words.Item1 + _s_words.Item2.ToString() + _s_words.Item1));
            return a;
        }
    }
}
