using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Lib;

namespace AdventOfCode.Solutions.Aoc2015.Day09;

class Solution : ISolution
{
    public object PartOne(IEnumerable<string> input)
    {
        return GetRoutes(input)
            .Select(r => r.distance)
            .Min();
    }

    public object PartTwo(IEnumerable<string> input)
    {
        return GetRoutes(input)
            .Select(r => r.distance)
            .Max();
    }

    private static IEnumerable<(string[] route, int distance)> GetRoutes(IEnumerable<string> input)
    {
        var distances = new Dictionary<(string, string), int>();
        var locations = new HashSet<string>();

        foreach (string line in input)
        {
            var match = Regex.Match(line, @"(\w+) to (\w+) = (\d+)");
            string from = match.Groups[1].Value;
            string to = match.Groups[2].Value;
            int distance = int.Parse(match.Groups[3].Value);

            distances.Add((from, to), distance);
            distances.Add((to, from), distance);
            locations.Add(from);
            locations.Add(to);
        }

        return Permutations.Generate(locations)
            .Select(r => (r, r.Zip(r.Skip(1), (a, b) => distances[(a, b)]).Sum()));
    }
}
