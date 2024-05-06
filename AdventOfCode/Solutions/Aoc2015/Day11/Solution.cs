using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Aoc2015.Day11;

public class Solution : ISolution
{
    public object PartOne(IEnumerable<string> input)
    {
        return Passwords(input.First())
            .Take(1)
            .Last();
    }

    public object PartTwo(IEnumerable<string> input)
    {
        return Passwords(input.First())
            .Take(2)
            .Last();
    }

    private static IEnumerable<string> Passwords(string seed)
    {
        return Generate(seed)
            .Where(s => HasValidChars(s) && HasStraight(s) && HasPairs(s));

        IEnumerable<string> Generate(string str)
        {
            var sb = new StringBuilder(str);

            while (true)
            {
                for (int i = sb.Length - 1; i >= 0; i--)
                {
                    if (sb[i] >= 'z')
                    {
                        sb[i] = 'a';
                    }
                    else
                    {
                        sb[i]++;
                        break;
                    }
                }

                yield return sb.ToString();
            }
        }

        bool HasValidChars(string str)
        {
            foreach (char ch in str)
            {
                if (ch is 'i' or 'o' or 'l')
                    return false;
            }

            return true;
        }

        bool HasStraight(string str)
        {
            for (var i = 0; i < str.Length - 2; i++)
            {
                if (str[i + 1] - str[i] == 1 && str[i + 2] - str[i] == 2)
                    return true;
            }

            return false;
        }

        bool HasPairs(string str)
        {
            var pairs = new HashSet<char>();

            for (var i = 0; i < str.Length - 1; i++)
            {
                if (str[i] == str[i + 1])
                    pairs.Add(str[i]);
            }

            return pairs.Count > 1;
        }
    }
}
