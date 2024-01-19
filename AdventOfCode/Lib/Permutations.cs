using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Lib;

static class Permutations
{
    /// <summary>
    /// Generates all possible permutations of the <paramref name="source"/> collection.
    /// </summary>
    public static IEnumerable<T[]> Generate<T>(IEnumerable<T> source)
    {
        var copy = source.ToArray();
        return Generate(copy, copy.Length);
    }

    /// <summary>
    /// Generates all possible permutations of the first <paramref name="size"/>
    /// elements in the <paramref name="source"/> array using Heap's algorithm
    /// (https://wikipedia.org/wiki/Heap%27s_algorithm).
    /// </summary>
    private static IEnumerable<T[]> Generate<T>(T[] source, int size)
    {
        if (size == 1)
        {
            yield return source;
        }
        else
        {
            foreach (var permutation in Generate(source, size - 1))
            {
                yield return permutation;
            }

            for (var i = 0; i < size - 1; i++)
            {
                if (size % 2 == 0)
                {
                    (source[i], source[size - 1]) = (source[size - 1], source[i]);
                }
                else
                {
                    (source[0], source[size - 1]) = (source[size - 1], source[0]);
                }

                foreach (var permutation in Generate(source, size - 1))
                {
                    yield return permutation;
                }
            }
        }
    }
}
