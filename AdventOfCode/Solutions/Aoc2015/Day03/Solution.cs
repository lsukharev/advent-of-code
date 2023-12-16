using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Aoc2015.Day03;

class Solution : ISolution
{
    private record struct Point(int X, int Y);

    public object PartOne(IEnumerable<string> input)
    {
        return Run(input.First(), 1);
    }

    public object PartTwo(IEnumerable<string> input)
    {
        return Run(input.First(), 2);
    }

    private static int Run(string moves, int numLocations)
    {
        var visited = new HashSet<Point> { new () };
        var locations = new Point[numLocations];
        var turn = 0;

        foreach (char move in moves)
        {
            locations[turn] = GetNextLocation(locations[turn], move);
            visited.Add(locations[turn]);
            turn = (turn + 1) % numLocations;
        }

        return visited.Count;
    }

    private static Point GetNextLocation(Point location, char move)
    {
        return move switch
        {
            '^' => location with { Y = location.Y + 1 },
            '>' => location with { X = location.X + 1 },
            'v' => location with { Y = location.Y - 1 },
            '<' => location with { X = location.X - 1 },
            _ => location
        };
    }
}
