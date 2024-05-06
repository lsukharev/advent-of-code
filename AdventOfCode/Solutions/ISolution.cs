using System.Collections.Generic;

namespace AdventOfCode.Solutions;

public interface ISolution
{
    object PartOne(IEnumerable<string> input);

    object PartTwo(IEnumerable<string> input);
}
