using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace AdventOfCode.Solutions.Aoc2015.Day12;

class Solution : ISolution
{
    public object PartOne(IEnumerable<string> input)
    {
        using var doc = JsonDocument.Parse(input.First());
        return SelectNumbers(doc.RootElement).Sum();
    }

    public object PartTwo(IEnumerable<string> input)
    {
        using var doc = JsonDocument.Parse(input.First());
        return SelectNumbers(doc.RootElement, IsObjectWithRedValue).Sum();

        bool IsObjectWithRedValue(JsonElement e) =>
            e.ValueKind == JsonValueKind.Object && e.EnumerateObject()
                .Any(m => m.Value.ValueKind == JsonValueKind.String && m.Value.GetString() == "red");
    }

    private static IEnumerable<int> SelectNumbers(JsonElement element, Func<JsonElement, bool>? ignore = null)
    {
        if (ignore is not null && ignore(element))
        {
            yield break;
        }

        if (element.ValueKind == JsonValueKind.Object)
        {
            foreach (int num in element.EnumerateObject().SelectMany(m => SelectNumbers(m.Value, ignore)))
            {
                yield return num;
            }
        }

        if (element.ValueKind == JsonValueKind.Array)
        {
            foreach (int num in element.EnumerateArray().SelectMany(m => SelectNumbers(m, ignore)))
            {
                yield return num;
            }
        }

        if (element.ValueKind == JsonValueKind.Number)
        {
            yield return element.GetInt32();
        }
    }
}
