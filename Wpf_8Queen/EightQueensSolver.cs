namespace Wpf_8Queen;

public sealed class EightQueensSolver
{
    public IReadOnlyList<IReadOnlyList<int>> Solve()
    {
        var solutions = new List<IReadOnlyList<int>>();
        var positions = new int[8];
        Array.Fill(positions, -1);
        Search(0, positions, solutions);
        return solutions;
    }

    private static void Search(int row, int[] positions, List<IReadOnlyList<int>> solutions)
    {
        if (row == 8)
        {
            solutions.Add(positions.ToArray());
            return;
        }

        for (var column = 0; column < 8; column++)
        {
            if (!CanPlace(row, column, positions))
                continue;

            positions[row] = column;
            Search(row + 1, positions, solutions);
            positions[row] = -1;
        }
    }

    private static bool CanPlace(int row, int column, int[] positions)
    {
        for (var previousRow = 0; previousRow < row; previousRow++)
        {
            var previousColumn = positions[previousRow];
            if (previousColumn == column ||
                Math.Abs(previousColumn - column) == row - previousRow)
                return false;
        }

        return true;
    }
}
