using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CommandLine;
using AdventOfCode.Solutions;

namespace AdventOfCode;

public class Options
{
    [Option('y', "year", Required = true, HelpText = "Set the yearly event")]
    public string Year { get; }

    [Option('d', "day", Required = true, HelpText = "Set the daily puzzle")]
    public string Day { get; }

    public Options(string year, string day)
    {
        this.Year = year;
        this.Day = day;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Parser.Default
            .ParseArguments<Options>(args)
            .WithParsed(o =>
            {
                var targetYear = $"Aoc{o.Year}";
                var targetDay = $"Day{o.Day}";

                Type targetSolution = Assembly.GetEntryAssembly()!.GetTypes()
                    .Where(t => t.IsClass && t.GetInterface(nameof(ISolution)) is not null)
                    .First(s =>
                    {
                        string[] fullName = s.FullName!.Split('.');
                        return fullName[2] == targetYear && fullName[3] == targetDay;
                    });

                var solution = Activator.CreateInstance(targetSolution) as ISolution;

                string[] input = File.ReadAllLines(
                    Path.Combine(AppContext.BaseDirectory, "Solutions", targetYear, targetDay, "input.txt"),
                    Encoding.UTF8);

                var stopwatch = Stopwatch.StartNew();
                object partOne = solution!.PartOne(input);
                stopwatch.Stop();
                Console.WriteLine("part one ({0} ms): {1}", stopwatch.Elapsed.TotalMilliseconds, partOne);

                stopwatch = Stopwatch.StartNew();
                object partTwo = solution.PartTwo(input);
                stopwatch.Stop();
                Console.WriteLine("part two ({0} ms): {1}", stopwatch.Elapsed.TotalMilliseconds, partTwo);
            });
    }
}
