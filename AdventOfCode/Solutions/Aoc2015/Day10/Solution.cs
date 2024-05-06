using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Aoc2015.Day10;

public class Solution : ISolution
{
    public object PartOne(IEnumerable<string> input)
    {
        return LookAndSay(input.ElementAt(0), 40)
            .Last()
            .Length;
    }

    public object PartTwo(IEnumerable<string> input)
    {
        return LookAndSay(input.ElementAt(0), 50)
            .Last()
            .Length;
    }

    private static IEnumerable<string> LookAndSay(string seed, int count)
    {
        string value = seed;

        for (var iteration = 0; iteration < count; iteration++)
        {
            var next = new StringBuilder("");

            for (var i = 0; i < value.Length;)
            {
                int j = i;
                while (j < value.Length && value[i] == value[j])
                {
                    j++;
                }

                next.Append($"{j - i}{value[i]}");
                i = j;
            }

            value = next.ToString();
            yield return value;
        }
    }
}
