using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Aoc2015.Day08;

public static class Solution
{
    public static void Run()
    {
        string[] input =
            File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, "Solutions", "Aoc2015", "Day08", "input.txt"));

        var stopwatch = Stopwatch.StartNew();
        int partOne = PartOne(input);
        stopwatch.Stop();
        Console.WriteLine("part one ({0} ms): {1}", stopwatch.Elapsed.TotalMilliseconds, partOne);

        stopwatch = Stopwatch.StartNew();
        int partTwo = PartTwo(input);
        stopwatch.Stop();
        Console.WriteLine("part two ({0} ms): {1}", stopwatch.Elapsed.TotalMilliseconds, partTwo);
    }

    private static int PartOne(IEnumerable<string> list)
    {
        return list
            .Select(l => l.Length - Unescape(l).Length)
            .Sum();

        string Unescape(string str)
        {
            string trimmed = str.Substring(1, str.Length - 2);
            return Regex.Unescape(trimmed);
        }
    }

    private static int PartTwo(IEnumerable<string> list)
    {
        return list
            .Select(l => Escape(l).Length - l.Length)
            .Sum();

        string Escape(string str)
        {
            string escaped = Regex.Replace(str, "\"|\\\\", match => $"\\{match.Value}");
            return $"\"{escaped}\"";
        }
    }
}
