using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2023.Day2
{
    internal static class Main
    {
        internal static async Task Run()
        {
            var config = new Dictionary<string, int>() { { "red", 12 }, { "green", 13 }, { "blue", 14 } };
            var f = await File.ReadAllLinesAsync("Day2/input.txt");
            var r = new Regex("(((\\d+) (\\w+),?\\s?)+;?\\s?)+");
            var gs = f.Select(b => r.Matches(b)[0].Groups[3].Captures.Select((a, i) => (int.Parse(a.Value), r.Matches(b)[0].Groups[4].Captures[i].Value)).GroupBy(a => a.Item2).Select(a => (a.Key, a.MaxBy(b => b.Item1).Item1)).ToDictionary(a => a.Key, a => a.Item2))
                .Select((a, i) => (i + 1, a));
            var res1 = gs
                .Where(a => a.a.All(b => config[b.Key] >= b.Value))
                .Sum(a => a.Item1);
            Console.WriteLine($"Part 1: {res1}");
            var res2 = gs.Select(a => a.a.Values.Aggregate(1, (b, c) => b * c)).Sum();
            Console.WriteLine($"Part 2: {res2}");
        }
    }
}
