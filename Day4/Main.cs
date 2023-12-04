using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2023.Day4
{
    internal static class Main
    {
        internal static async Task Run()
        {
            var f = await File.ReadAllLinesAsync("Day4/input.txt");
            var d = f.Select(a => a.Split(':')[1].Split('|').Select(b => b.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet()).ToArray())
                .Select(a => a[0].Intersect(a[1]).Count())
                .Select(a => a == 0 ? 0 : Math.Pow(2, a-1))
                .Sum();
            Console.WriteLine($"Part 1: {d}");
            var e = f.Select(a => (c: 1, v: a.Split(':')[1].Split('|').Select(b => b.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet()).ToArray()))
                .Select(a => (a.c, a.v, w: a.v[0].Intersect(a.v[1]).Count()))
                .ToArray();
            for (var i = 0; i < e.Length; i++)
                for (var j = 1; j <= e[i].w; j++)
                    e[i + j].c += e[i].c;
            var g = e.Select(a => a.c).Sum();
            Console.WriteLine($"Part 2: {g}");
        }
    }
}
