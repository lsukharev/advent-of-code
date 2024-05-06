using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Aoc2015.Day05;

public class Solution : ISolution
{
    public object PartOne(IEnumerable<string> lines)
    {
        var threeVowels = new Regex("([aeiou].*){3,}");
        var doubleLetter = new Regex(@"(\w)\1");
        var invalidStrings = new Regex("ab|cd|pq|xy");

        return lines.Count(line =>
            threeVowels.IsMatch(line) && doubleLetter.IsMatch(line) && !invalidStrings.IsMatch(line));
    }

    public object PartTwo(IEnumerable<string> lines)
    {
        var twoLetterPair = new Regex(@"(\w\w).*\1");
        var repeatLetter = new Regex(@"(\w)\w\1");

        return lines.Count(line => twoLetterPair.IsMatch(line) && repeatLetter.IsMatch(line));
    }
}
