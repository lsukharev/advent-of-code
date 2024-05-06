using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Aoc2015.Day07;

public class Solution : ISolution
{
    private class Circuit : Dictionary<string, Func<int>>;

    public object PartOne(IEnumerable<string> instructions)
    {
        Circuit circuit = MakeCircuit(instructions);
        return GetSignal("a", circuit);
    }

    public object PartTwo(IEnumerable<string> input)
    {
        string[] instructions = input.ToArray();
        Circuit circuit = MakeCircuit(instructions.Append($"{PartOne(instructions)} -> b"));
        return GetSignal("a", circuit);
    }

    private static Circuit MakeCircuit(IEnumerable<string> instructions)
    {
        return instructions.Aggregate(new Circuit(), (circuit, instruction) =>
        {
            Match match = Regex.Match(instruction,
                @"(?<source>NOT \w+|\w+ (AND|OR|LSHIFT|RSHIFT) \w+|\w+) -> (?<target>\w+)");

             if (!match.Success)
                 throw new ArgumentException($"Unrecognized instruction: {instruction}");

             string[] source = match.Groups["source"].Value.Split(' ');
             string target = match.Groups["target"].Value;

             circuit[target] = source.Length switch
             {
                 3 => () =>
                 {
                     int input1 = GetSignal(source[0], circuit);
                     int input2 = GetSignal(source[2], circuit);
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
                     circuit[target] = () => signal;
                     return signal;
                 },
                 _ => throw new ArgumentException($"Unrecognized instruction: {instruction}")
             };

             return circuit;
        });
    }

    private static int GetSignal(string wire, Circuit circuit)
    {
         if (int.TryParse(wire, out int v))
             return v;

         if (circuit.TryGetValue(wire, out Func<int>? cv))
             return cv();

         throw new ArgumentException($"Unable to get signal from wire: {wire}");
    }
}
