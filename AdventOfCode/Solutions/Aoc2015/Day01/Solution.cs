using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Aoc2015.Day01;

public static class Solution
{
    public static void Run()
    {
        string[] input = File.ReadAllLines(
            Path.Combine(AppContext.BaseDirectory, "Solutions", "Aoc2015", "Day01", "input.txt"),
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

    private static int PartOne(string instructions)
    {
        return instructions.Sum(step => step switch
        {
            '(' => 1,
            ')' => -1,
            _ => 0
        });
    }

    private static int PartTwo(string instructions)
    {
        var floor = 0;

        for (var index = 0; index < instructions.Length; index++)
        {
            floor += instructions[index] switch
            {
                '(' => 1,
                ')' => -1,
                _ => 0
            };

            if (floor == -1)
            {
                return index + 1;
            }
        }

        return -1;
    }
}
