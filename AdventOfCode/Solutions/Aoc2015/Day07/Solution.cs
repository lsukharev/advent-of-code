using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Aoc2015.Day07;

public static class Solution
{
    public static void Run()
    {
        string[] input =
            File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, "Solutions", "Aoc2015", "Day07", "input.txt"));

        var stopwatch = Stopwatch.StartNew();
        int partOne = PartOne(input);
        stopwatch.Stop();
        Console.WriteLine("part one ({0} ms): {1}", stopwatch.Elapsed.TotalMilliseconds, partOne);

        stopwatch = Stopwatch.StartNew();
        int partTwo = PartTwo(input);
        stopwatch.Stop();
        Console.WriteLine("part two ({0} ms): {1}", stopwatch.Elapsed.TotalMilliseconds, partTwo);
    }

    private static int PartOne(IEnumerable<string> instructions)
    {
        var circuit = MakeCircuit(instructions);
        return GetSignal("a", circuit);
    }

    private static int PartTwo(string[] instructions)
    {
        var circuit = MakeCircuit(instructions.Append($"{PartOne(instructions)} -> b"));
        return GetSignal("a", circuit);
    }

    private static Dictionary<string, Func<int>> MakeCircuit(IEnumerable<string> instructions)
    {
        var format = new Regex(@"(?<source>NOT \w+|\w+ (AND|OR|LSHIFT|RSHIFT) \w+|\w+) -> (?<target>\w+)");

        return instructions.Aggregate(new Dictionary<string, Func<int>>(), (circuit, instruction) =>
        {
             var match = format.Match(instruction);

             if (!match.Success)
             {
                 throw new ArgumentException($"Unrecognized instruction: {instruction}");
             }

             string[] source = match.Groups["source"].Value.Split(' ');
             string target = match.Groups["target"].Value;

             circuit[target] = source.Length switch
             {
                 3 => () =>
                 {
                     int input1 = GetSignal(source[0], circuit);
                     circuit[source[0]] = () => input1;

                     int input2 = GetSignal(source[2], circuit);
                     circuit[source[2]] = () => input2;

                     int signal = source[1] switch
                     {
                         "AND" => input1 & input2,
                         "OR" => input1 | input2,
                         "LSHIFT" => input1 << input2,
                         "RSHIFT" => input1 >> input2,
                         _ => throw new ArgumentException($"Unrecognized operation: {source[1]}")
                     };
                     circuit[target] = () => signal;
                     return signal;
                 },
                 2 => () =>
                 {
                     int input = GetSignal(source[1], circuit);
                     circuit[source[1]] = () => input;

                     int signal = source[0] switch
                     {
                         "NOT" => ~input,
                         _ => throw new ArgumentException($"Unrecognized operation: {source[0]}")
                     };
                     circuit[target] = () => signal;
                     return signal;
                 },
                 1 => () =>
                 {
                     int signal = GetSignal(source[0], circuit);
                     circuit[source[0]] = () => signal;
                     circuit[target] = () => signal;
                     return signal;
                 },
                 _ => throw new ArgumentException($"Unrecognized instruction: {instruction}")
             };

             return circuit;
        });
    }

    private static int GetSignal(string wire, IReadOnlyDictionary<string, Func<int>> circuit)
    {
         if (int.TryParse(wire, out int v))
         {
             return v;
         }

         if (circuit.TryGetValue(wire, out var cv))
         {
             return cv();
         }

         throw new ArgumentException($"Unable to get signal from wire: {wire}");
    }
}
