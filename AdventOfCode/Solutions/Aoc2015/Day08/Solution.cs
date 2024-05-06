using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Aoc2015.Day08;

public class Solution : ISolution
{
    public object PartOne(IEnumerable<string> list)
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

    public object PartTwo(IEnumerable<string> list)
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
