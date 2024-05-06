using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Aoc2015.Day01;

public class Solution : ISolution
{
    public object PartOne(IEnumerable<string> input)
    {
        return Parse(input.First()).Last().floor;
    }

    public object PartTwo(IEnumerable<string> input)
    {
        return Parse(input.First()).First(i => i.floor == -1).position;
    }

    private static IEnumerable<(int position, int floor)> Parse(string instructions)
    {
        var floor = 0;

        for (var i = 0; i < instructions.Length; i++)
        {
            floor += instructions[i] switch
            {
                '(' => 1,
                ')' => -1,
                _ => 0
            };

            yield return (position: i + 1, floor);
        }
    }
}
