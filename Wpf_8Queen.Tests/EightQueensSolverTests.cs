using Xunit;
using Wpf_8Queen;

namespace Wpf_8Queen.Tests;

public class EightQueensSolverTests
{
    private readonly EightQueensSolver _solver = new();

    [Fact]
    public void Solve_ReturnsExactly92Solutions()
    {
        var solutions = _solver.Solve();

        Assert.Equal(92, solutions.Count);
    }

    [Fact]
    public void Solve_ReturnsValidSolutions()
    {
        var solutions = _solver.Solve();

        Assert.All(solutions, solution =>
        {
            Assert.Equal(8, solution.Count);
            Assert.All(solution, column => Assert.InRange(column, 0, 7));

            for (var row1 = 0; row1 < 8; row1++)
            {
                for (var row2 = row1 + 1; row2 < 8; row2++)
                {
                    Assert.NotEqual(solution[row1], solution[row2]);
                    Assert.NotEqual(
                        Math.Abs(solution[row1] - solution[row2]),
                        row2 - row1);
                }
            }
        });
    }

    [Fact]
    public void Solve_ReturnsUniqueSolutions()
    {
        var keys = _solver.Solve()
            .Select(solution => string.Join(",", solution))
            .ToList();

        Assert.Equal(keys.Count, keys.Distinct().Count());
    }
}
