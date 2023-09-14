using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Aoc2015.Day02;

public static class Solution
{
    public static void Run()
    {
        string[] input = File.ReadAllLines(
            Path.Combine(AppContext.BaseDirectory, "Solutions", "Aoc2015", "Day02", "input.txt"),
            Encoding.UTF8);

        var stopwatch = Stopwatch.StartNew();
        int partOne = PartOne(input);
        stopwatch.Stop();
        Console.WriteLine("part one ({0} ms): {1}", stopwatch.Elapsed.TotalMilliseconds, partOne);

        stopwatch = Stopwatch.StartNew();
        int partTwo = PartTwo(input);
        stopwatch.Stop();
        Console.WriteLine("part two ({0} ms): {1}", stopwatch.Elapsed.TotalMilliseconds, partTwo);
    }

    private static int PartOne(IEnumerable<string> sizes)
    {
        return sizes
            .Select(size => size
                .Split('x')
                .Select(int.Parse)
                .OrderBy(s => s)
                .ToArray())
            .Sum(d => 2 * (d[0] * d[1] + d[0] * d[2] + d[1] * d[2]) + d[0] * d[1]);
    }

    private static int PartTwo(IEnumerable<string> sizes)
    {
        return sizes
            .Select(size => size
                .Split('x')
                .Select(int.Parse)
                .OrderBy(s => s)
                .ToArray())
            .Sum(d => 2 * (d[0] + d[1]) + d[0] * d[1] * d[2]);
    }
}
