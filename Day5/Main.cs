using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2023.Day5
{
    public static class Main
    {
        public static async Task Run()
        {
            var f = await File.ReadAllLinesAsync("Day5/input.txt");
            IEnumerable<ulong> seeds = f[0].Substring(6).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => ulong.Parse(x));

            var maps = new List<List<(ulong d, ulong s, ulong l)>>();
            var curMap = new List<(ulong d, ulong s, ulong l)>();
            foreach (var line in f.Skip(3))
            {
                if (line.Trim() == string.Empty)
                    continue;
                else if (line.Contains(':'))
                {
                    maps.Add(curMap);
                    curMap = new List<(ulong d, ulong s, ulong l)>();
                    continue;
                }

                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var x = ulong.Parse(parts[0]);
                var y = ulong.Parse(parts[1]);
                var z = ulong.Parse(parts[2]);
                curMap.Add((d: x, s: y, l: z));
            }
            maps.Add(curMap);

            var q = seeds.Select(seed =>
            {
                ulong val = seed;
                foreach (var map in maps)
                {
                    foreach (var (d, s, l) in map)
                    {
                        if (val >= s && val <= s + l)
                        {
                            val = d + (val - s);
                            break;
                        }
                    }
                }
                return (seed, val);
            }).MinBy(x => x.val).val;

            Console.WriteLine($"Part 1: {q}");



            var seedsF = f[0].Substring(6).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => ulong.Parse(x)).Batch(2).Select(a=>(a.First(), a.Last()));
            var g = seedsF.SelectMany(s => GetMapped(s.Item1, s.Item2, maps, 0)).Min();
            Console.WriteLine($"Part 2: {g}");
        }

        public static IEnumerable<ulong> GetMapped(ulong s, ulong l, List<List<(ulong d, ulong s, ulong l)>> maps, int i)
        {
            var matched = new List<(ulong, ulong)>();
            ulong seedMin = s;
            ulong seedMax = s + l - 1;
            foreach (var map in maps[i])
            {
                ulong mapMin = map.s;
                ulong mapMax = map.s + map.l - 1;
                if (seedMin <= mapMax && seedMax >= mapMin)
                {
                    if (i >= maps.Count - 1)
                    {
                        yield return map.d + (Math.Max(seedMin, mapMin) - map.s);
                        matched.Add((Math.Max(seedMin, mapMin), Math.Min(seedMax, mapMax)));
                    }
                    else
                    {
                        var inMin = map.d + (Math.Max(seedMin, mapMin) - map.s);
                        var inMax = map.d + (Math.Min(seedMax, mapMax) - map.s);
                        foreach (var x in GetMapped(inMin, inMax - inMin + 1, maps, i + 1))
                            yield return x;
                        matched.Add((Math.Max(seedMin, mapMin), Math.Min(seedMax, mapMax)));
                    }
                } 
            }
            matched = matched.OrderBy(x => x.Item1).ToList();
            foreach (var (min, max) in matched)
            {
                if (min > seedMin)
                {
                    if (i >= maps.Count - 1)
                    {
                        yield return seedMin;
                        break;
                    }
                    else
                    {
                        foreach (var x in GetMapped(seedMin, min - seedMin + 1, maps, i + 1))
                            yield return x;
                    }
                }
                seedMin = max + 1;
            }
            if (seedMin <= seedMax)
            {
                if (i >= maps.Count - 1)
                {
                    yield return seedMin;
                }
                else
                {
                    foreach (var x in GetMapped(seedMin, seedMax - seedMin + 1, maps, i + 1))
                        yield return x;
                }
            }
        }
    }
}

static class Ex
{
    public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(
                  this IEnumerable<TSource> source, int size)
    {
        TSource[] bucket = null;
        var count = 0;

        foreach (var item in source)
        {
            if (bucket == null)
                bucket = new TSource[size];

            bucket[count++] = item;
            if (count != size)
                continue;

            yield return bucket;

            bucket = null;
            count = 0;
        }

        if (bucket != null && count > 0)
            yield return bucket.Take(count).ToArray();
    }
}