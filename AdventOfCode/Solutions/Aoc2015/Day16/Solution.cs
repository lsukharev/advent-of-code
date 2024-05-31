using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Aoc2015.Day16;

public class Solution : ISolution
{
    private record Aunt(int Id, Dictionary<string, int> Characteristics);

    private static readonly Dictionary<string, int> _target = new()
    {
        ["children"] = 3,
        ["cats"] = 7,
        ["samoyeds"] = 2,
        ["pomeranians"] = 3,
        ["akitas"] = 0,
        ["vizslas"] = 0,
        ["goldfish"] = 5,
        ["trees"] = 3,
        ["cars"] = 2,
        ["perfumes"] = 1
    };

    public object PartOne(IEnumerable<string> input)
    {
        return Parse(input)
            .First(a => a.Characteristics.All(c => c.Value == _target[c.Key]))
            .Id;
    }

    public object PartTwo(IEnumerable<string> input)
    {
        return Parse(input)
            .First(i => i.Characteristics.All(c => c.Key switch
            {
                "cats" or "trees" => c.Value > _target[c.Key],
                "pomeranians" or "goldfish" => c.Value < _target[c.Key],
                _ => c.Value == _target[c.Key]
            }))
            .Id;
    }

    private static IEnumerable<Aunt> Parse(IEnumerable<string> input)
    {
        return input.Select(line =>
        {
            Match match = Regex.Match(line, @"Sue (\d+): (\w+: \d+), (\w+: \d+), (\w+: \d+)");
            return new Aunt(
                int.Parse(match.Groups[1].Value),
                match.Groups
                    .Cast<Group>()
                    .Skip(2)
                    .Select(g => g.Value.Split(": "))
                    .ToDictionary(p => p[0], p => int.Parse(p[1])));
        });
    }
}
