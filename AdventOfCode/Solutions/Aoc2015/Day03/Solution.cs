using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AdventOfCode.Solutions.Aoc2015.Day03;

public readonly struct Point
{
    public int X { get; }

    public int Y { get; }

    public Point(int x, int y) => (X, Y) = (x, y);
}

public static class Solution
{
    public static void Run()
    {
        string[] input = File.ReadAllLines(
            Path.Combine(AppContext.BaseDirectory, "Solutions", "Aoc2015", "Day03", "input.txt"),
            Encoding.UTF8);

        var stopwatch = Stopwatch.StartNew();
        int partOne = PartOne(input[0]);
        stopwatch.Stop();
        Console.WriteLine("part one ({0} ms): {1}", stopwatch.Elapsed.TotalMilliseconds, partOne);

        stopwatch = Stopwatch.StartNew();
        int partTwo = PartTwo(input[0]);
        stopwatch.Stop();
        Console.WriteLine("part two ({0} ms): {1}", stopwatch.Elapsed.TotalMilliseconds, partTwo);
    }

    private static int PartOne(string moves)
    {
        var location = new Point(0, 0);
        var visited = new HashSet<Point> { location };

        foreach (char move in moves)
        {
            location = GetNextLocation(location, move);
            visited.Add(location);
        }

        return visited.Count;
    }

    private static int PartTwo(string moves)
    {
        var santaLocation = new Point(0, 0);
        var robotLocation = santaLocation;
        var visited = new HashSet<Point> { santaLocation };

        for (var index = 0; index < moves.Length; index++)
        {
            if (index % 2 == 0)
            {
                santaLocation = GetNextLocation(santaLocation, moves[index]);
                visited.Add(santaLocation);
            }
            else
            {
                robotLocation = GetNextLocation(robotLocation, moves[index]);
                visited.Add(robotLocation);
            }
        }

        return visited.Count;
    }

    private static Point GetNextLocation(Point location, char move)
    {
        return move switch
        {
            '^' => new Point(location.X, location.Y + 1),
            '>' => new Point(location.X + 1, location.Y),
            'v' => new Point(location.X, location.Y - 1),
            '<' => new Point(location.X - 1, location.Y),
            _ => location
        };
    }
}
