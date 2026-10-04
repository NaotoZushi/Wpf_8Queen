using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wpf_8Queen;

public partial class MainWindow : Window
{
    private readonly EightQueensSolver _solver = new();
    private IReadOnlyList<IReadOnlyList<int>> _solutions = Array.Empty<IReadOnlyList<int>>();
    private int _solutionIndex;

    public MainWindow()
    {
        InitializeComponent();
        BuildBoard();
    }

    private void BuildBoard()
    {
        Board.Children.Clear();

        for (var row = 0; row < 8; row++)
        {
            for (var column = 0; column < 8; column++)
            {
                var cell = new Border
                {
                    Background = (row + column) % 2 == 0
                        ? Brushes.Beige
                        : Brushes.SaddleBrown
                };

                cell.Child = new TextBlock
                {
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 34,
                    FontWeight = FontWeights.Bold
                };

                Board.Children.Add(cell);
            }
        }
    }

    private void ShowSolution(int index)
    {
        if (_solutions.Count == 0)
            return;

        _solutionIndex = index;
        var solution = _solutions[index];

        for (var row = 0; row < 8; row++)
        {
            for (var column = 0; column < 8; column++)
            {
                var cell = (Border)Board.Children[row * 8 + column];
                var text = (TextBlock)cell.Child!;
                text.Text = solution[row] == column ? "♛" : string.Empty;
            }
        }

        StatusText.Text = $"解 {_solutionIndex + 1} / {_solutions.Count}";
    }

    private void SolveButton_Click(object sender, RoutedEventArgs e)
    {
        _solutions = _solver.Solve();
        ShowSolution(0);
    }

    private void NextButton_Click(object sender, RoutedEventArgs e)
    {
        if (_solutions.Count == 0)
        {
            _solutions = _solver.Solve();
        }

        ShowSolution((_solutionIndex + 1) % _solutions.Count);
    }

    private void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        _solutions = Array.Empty<IReadOnlyList<int>>();
        _solutionIndex = 0;

        for (var i = 0; i < Board.Children.Count; i++)
        {
            var text = (TextBlock)((Border)Board.Children[i]).Child!;
            text.Text = string.Empty;
        }

        StatusText.Text = "解くボタンで解を表示します。";
    }
}
