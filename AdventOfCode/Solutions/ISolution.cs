using System.Collections.Generic;

namespace AdventOfCode.Solutions;

interface ISolution
{
    object PartOne(IEnumerable<string> input);

    object PartTwo(IEnumerable<string> input);
}
