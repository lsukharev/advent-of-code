using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Solutions.Aoc2015.Day04;

public static class Solution
{
    public static void Run()
    {
        string[] input =
            File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, "Solutions", "Aoc2015", "Day04", "input.txt"));

        var stopwatch = Stopwatch.StartNew();
        int partOne = PartOne(input[0]);
        stopwatch.Stop();
        Console.WriteLine("part one ({0} ms): {1}", stopwatch.Elapsed.TotalMilliseconds, partOne);

        stopwatch = Stopwatch.StartNew();
        int partTwo = PartTwo(input[0]);
        stopwatch.Stop();
        Console.WriteLine("part two ({0} ms): {1}", stopwatch.Elapsed.TotalMilliseconds, partTwo);
    }

    private static int PartOne(string key) => FindHash(key, "00000");

    private static int PartTwo(string key) => FindHash(key, "000000");

    private static int FindHash(string key, string prefix)
    {
        foreach (int number in Enumerable.Range(0, int.MaxValue))
        {
            byte[] bytes = MD5.HashData(Encoding.ASCII.GetBytes(key + number));
            string hash = string.Join(string.Empty, bytes.Select(b => b.ToString("X2")));

            if (hash.StartsWith(prefix))
            {
                return number;
            }
        }

        return -1;
    }
}
