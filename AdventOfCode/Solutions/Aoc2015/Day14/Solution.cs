using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Aoc2015.Day14;

public class Solution : ISolution
{
    private record Reindeer(int Speed, int FlightTime, int RestTime);

    public object PartOne(IEnumerable<string> input)
    {
        return Race(Parse(input), 2503).Max(s => s.distance);
    }

    public object PartTwo(IEnumerable<string> input)
    {
        return Race(Parse(input), 2503).Max(s => s.points);
    }

    private static Reindeer[] Parse(IEnumerable<string> input)
    {
        return input
            .Aggregate(new List<Reindeer>(), (reindeer, line) =>
            {
                var match = Regex.Match(line,
                    @"(\w+) can fly (\d+) km/s for (\d+) seconds, but then must rest for (\d+) seconds.");
                int speed = int.Parse(match.Groups[2].Value);
                int flightTime = int.Parse(match.Groups[3].Value);
                int restTime = int.Parse(match.Groups[4].Value);

                reindeer.Add(new Reindeer(speed, flightTime, restTime));
                return reindeer;
            })
            .ToArray();
    }

    private static (int distance, int points)[] Race(Reindeer[] reindeer, int raceTime)
    {
        var state = new (int distance, int points)[reindeer.Length];

        for (var t = 0; t < raceTime; t++)
        {
            for (var i = 0; i < reindeer.Length; i++)
            {
                (int speed, int flightTime, int restTime) = reindeer[i];
                (int distance, int points) = state[i];

                if (t % (flightTime + restTime) < flightTime)
                    state[i] = (distance + speed, points);
            }

            int maxDistance = state.Max(s => s.distance);

            for (var i = 0; i < reindeer.Length; i++)
            {
                (int distance, int points) = state[i];

                if (distance == maxDistance)
                    state[i] = (distance, points + 1);
            }
        }

        return state;
    }
}
