using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2023.Day6
{
    public static class Main
    {
        public static async Task Run()
        {
            var f = await File.ReadAllLinesAsync("Day6/input.txt");
            {
                var times = f[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse);
                var distances = f[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse);
                var tds = times.Zip(distances, (t, d) => (t, d));
                var winProduct = 1;
                foreach (var item in tds)
                {
                    var wins = 0;
                    for (int i = 1; i < item.t; i++)
                    {
                        var distance = i * (item.t - i);
                        if (distance > item.d)
                        {
                            wins++;
                        }
                    }
                    winProduct *= wins;
                }
                Console.WriteLine($"Part 1: {winProduct}");
            }
            {
                var time = ulong.Parse(f[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Aggregate("", (a,b)=>a+b));
                var dis = ulong.Parse(f[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Aggregate("", (a, b) => a + b));
                var tds = new[] { (t: time, d: dis) };
                ulong winProduct = 1;
                foreach (var item in tds)
                {
                    ulong wins = 0;
                    for (ulong i = 1; i < item.t; i++)
                    {
                        ulong distance = i * (item.t - i);
                        if (distance > item.d)
                        {
                            wins++;
                        }
                    }
                    winProduct *= wins;
                }

                Console.WriteLine($"Part 2: {winProduct}");
            }
        }

    }
}
