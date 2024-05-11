using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Aoc2015.Day15;

public class Solution : ISolution
{
    private record Ingredient(int Capacity, int Durability, int Flavor, int Texture, int Calories);

    public object PartOne(IEnumerable<string> input)
    {
        return Recipe(Parse(input), null);
    }

    public object PartTwo(IEnumerable<string> input)
    {
        return Recipe(Parse(input), 500);
    }

    private static int Recipe(Ingredient[] ingredients, int? calorieLimit)
    {
        return Partition(100, ingredients.Length)
            .Aggregate(0, (score, amounts) =>
            {
                var capacity = 0;
                var durability = 0;
                var flavor = 0;
                var texture = 0;
                var calories = 0;

                for (var i = 0; i < ingredients.Length; i++)
                {
                    Ingredient ingredient = ingredients[i];
                    int amount = amounts[i];

                    capacity += amount * ingredient.Capacity;
                    durability += amount * ingredient.Durability;
                    flavor += amount * ingredient.Flavor;
                    texture += amount * ingredient.Texture;
                    calories += amount * ingredient.Calories;
                }

                if (calories > calorieLimit)
                    return score;

                capacity = Math.Max(capacity, 0);
                durability = Math.Max(durability, 0);
                flavor = Math.Max(flavor, 0);
                texture = Math.Max(texture, 0);

                return Math.Max(score, capacity * durability * flavor * texture);
            });
    }

    private static Ingredient[] Parse(IEnumerable<string> input)
    {
        return input
            .Aggregate(new List<Ingredient>(), (ingredients, line) =>
            {
                Match match = Regex.Match(line,
                    @"\w+: capacity (-?\d+), durability (-?\d+), flavor (-?\d+), texture (-?\d+), calories (-?\d+)");

                int capacity = int.Parse(match.Groups[1].Value);
                int durability = int.Parse(match.Groups[2].Value);
                int flavor = int.Parse(match.Groups[3].Value);
                int texture = int.Parse(match.Groups[4].Value);
                int calories = int.Parse(match.Groups[5].Value);

                ingredients.Add(new Ingredient(capacity, durability, flavor, texture, calories));
                return ingredients;
            })
            .ToArray();
    }

    private static IEnumerable<int[]> Partition(int num, int parts)
    {
        if (parts == 1)
        {
            yield return [num];
        }
        else
        {
            for (var i = 0; i <= num; i++)
            {
                foreach (int[] rest in Partition(num - i, parts - 1))
                {
                    yield return [..rest, i];
                }
            }
        }
    }
}
