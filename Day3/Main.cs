using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2023.Day3
{
    internal static class Main
    {
        internal static async Task Run()
        {
            var f = await File.ReadAllLinesAsync("Day3/input.txt");
            var n = f.SelectMany((a, i) => new Regex("(\\d+)").Matches(a).Select(b=>(new { x=b.Index,y=i,l=b.Length,v=b.Value }))).ToArray();
            var s = f.SelectMany((a, i) => new Regex("([^\\d\\.]+)").Matches(a).Select(b=>(new { x=b.Index,y=i }))).ToArray();
            var r = n.Where(a => s.Where(b => a.x - 1 <= b.x && b.x <= a.x + a.l && a.y-1<=b.y && b.y <= a.y + 1).Any()).Sum(a => int.Parse(a.v));
            Console.WriteLine($"Part 1: {r}");
            var y = f.SelectMany((a, i) => new Regex("(\\*+)").Matches(a).Select(b => (new { x = b.Index, y = i })))
                .Select(b => n.Where(a => a.x - 1 <= b.x && b.x <= a.x + a.l && a.y - 1 <= b.y && b.y <= a.y + 1).Select(z => new { c = 1, p = int.Parse(z.v) })
                .Aggregate(new { c = 0, p = 1 }, (q, w) => new { c = q.c + w.c, p = q.p * w.p }))
                .Sum(a => a.p);
            Console.WriteLine($"Part 2: {y}");
        }
    }
}
