using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Aoc2015.Day04;

class Solution : ISolution
{
    public object PartOne(IEnumerable<string> input)
    {
        return FindHash(input.First(), "00000");
    }

    public object PartTwo(IEnumerable<string> input)
    {
        return FindHash(input.First(), "000000");
    }

    private static int FindHash(string key, string prefix)
    {
        var bag = new ConcurrentBag<int>();

        Parallel.ForEach(
            Enumerable.Range(0, int.MaxValue),
            (num, state) =>
            {
                byte[] bytes = MD5.HashData(Encoding.ASCII.GetBytes(key + num));
                string hash = string.Join("", bytes.Select(b => b.ToString("X2")));

                if (!hash.StartsWith(prefix))
                    return;

                bag.Add(num);
                state.Stop();
            });

        return bag.First();
    }
}
