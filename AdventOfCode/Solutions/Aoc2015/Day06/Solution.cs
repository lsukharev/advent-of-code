using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Aoc2015.Day06;

public class Solution : ISolution
{
    public object PartOne(IEnumerable<string> instructions)
    {
        return Apply(instructions, new Dictionary<string, Func<int, int>>
        {
            { "turn on", _ => 1 },
            { "turn off", _ => 0 },
            { "toggle", l => l == 0 ? 1 : 0 }
        });
    }

    public object PartTwo(IEnumerable<string> instructions)
    {
        return Apply(instructions, new Dictionary<string, Func<int, int>>
        {
            { "turn on", l => l + 1 },
            { "turn off", l => l == 0 ? 0 : l - 1 },
            { "toggle", l => l + 2 }
        });
    }

    private static int Apply(IEnumerable<string> instructions, Dictionary<string, Func<int, int>> actions)
    {
        return instructions
            .Aggregate(new int[1000, 1000], (grid, instruction) =>
            {
                Match match = Regex.Match(instruction,
                    @"(?<action>.*) (?<pointOne>\d+,\d+) through (?<pointTwo>\d+,\d+)");

                string action = match.Groups["action"].Value;
                string[] pointOne = match.Groups["pointOne"].Value.Split(',');
                string[] pointTwo = match.Groups["pointTwo"].Value.Split(',');

                if (!actions.TryGetValue(action, out Func<int, int>? doAction))
                    return grid;

                for (int row = int.Parse(pointOne[1]); row <= int.Parse(pointTwo[1]); row++)
                {
                    for (int col = int.Parse(pointOne[0]); col <= int.Parse(pointTwo[0]); col++)
                    {
                        grid[row, col] = doAction(grid[row, col]);
                    }
                }

                return grid;
            })
            .Cast<int>()
            .Sum();
    }
}
