using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Lib;

namespace AdventOfCode.Solutions.Aoc2015.Day13;

public class Solution : ISolution
{
    public object PartOne(IEnumerable<string> input)
    {
        return OptimalHappiness(input, false);
    }

    public object PartTwo(IEnumerable<string> input)
    {
        return OptimalHappiness(input, true);
    }

    private static int OptimalHappiness(IEnumerable<string> input, bool includeSelf)
    {
        var people = new HashSet<string>();
        var happiness = new Dictionary<(string, string), int>();

        foreach (string line in input)
        {
            Match match = Regex.Match(line, @"(\w+) would (gain|lose) (\d+) happiness units by sitting next to (\w+).");
            string personA = match.Groups[1].Value;
            string personB = match.Groups[4].Value;
            bool gain = match.Groups[2].Value == "gain";
            int units = int.Parse(match.Groups[3].Value);

            people.Add(personA);
            people.Add(personB);
            happiness.Add((personA, personB), gain ? +units : -units);
        }

        if (includeSelf)
            people.Add("self");

        return Permutations.Generate(people)
            .Select(a =>
            {
                var change = 0;
                for (var x = 0; x < a.Length; x++)
                {
                    string leftNeighbor = x == 0 ? a[^1] : a[x - 1];
                    string rightNeighbor = x == a.Length - 1 ? a[0] : a[x + 1];
                    change += happiness.GetValueOrDefault((a[x], leftNeighbor), 0);
                    change += happiness.GetValueOrDefault((a[x], rightNeighbor), 0);
                }

                return change;
            })
            .Max();
    }
}
