using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Aoc2015.Day02;

class Solution : ISolution
{
    public object PartOne(IEnumerable<string> dimensions)
    {
        return Parse(dimensions)
            .Sum(d => 2 * (d[0] * d[1] + d[0] * d[2] + d[1] * d[2]) + d[0] * d[1]);
    }

    public object PartTwo(IEnumerable<string> dimensions)
    {
        return Parse(dimensions)
            .Sum(d => 2 * (d[0] + d[1]) + d[0] * d[1] * d[2]);
    }

    private static IEnumerable<int[]> Parse(IEnumerable<string> dimensions)
    {
        return dimensions
            .Select(dim => dim
                .Split('x')
                .Select(int.Parse)
                .OrderBy(d => d)
                .ToArray());
    }
}
