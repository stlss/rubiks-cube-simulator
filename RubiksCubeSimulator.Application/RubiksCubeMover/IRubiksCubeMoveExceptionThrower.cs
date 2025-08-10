using RubiksCubeSimulator.Application.RubiksCubeMover.Checkers;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;

namespace RubiksCubeSimulator.Application.RubiksCubeMover;

public interface IRubiksCubeMoveExceptionThrower
{
    public void ThrowExceptionIfRubiksCubeIsNotCorrect(RubiksCube cube);

    public void ThrowExceptionIfRubiksCubeMoveIsNotCorrect(RubiksCubeMoveBase move, int cubeDimension);

    public void ThrowExceptionIfRubiksCubeMovesIsNotCorrect(IEnumerable<RubiksCubeMoveBase> moves, int cubeDimension);
}

internal sealed class RubiksCubeMoveExceptionThrower(
    IRubiksCubeChecker cubeChecker,
    IRubiksCubeMoveChecker cubeMoveChecker)
    : IRubiksCubeMoveExceptionThrower
{
    public void ThrowExceptionIfRubiksCubeIsNotCorrect(RubiksCube cube)
    {
        if (cubeChecker.IsCorrectRubiksCube(cube)) return;

        const string message = "Rubik's cube is not correct. " +
                               "Rubik's cube dimension must be greater or equal '2' and every cube face dimension must equal cube dimension.";

        throw new ArgumentException(message);
    }

    public void ThrowExceptionIfRubiksCubeMoveIsNotCorrect(RubiksCubeMoveBase move, int cubeDimension)
    {
        var correct = IsCorrectRubiksCubeMove(move, cubeDimension);

        if (correct) return;

        ThrowRubiksCubeMoveException(move);
    }

    public void ThrowExceptionIfRubiksCubeMovesIsNotCorrect(IEnumerable<RubiksCubeMoveBase> moves, int cubeDimension)
    {
        var incorrectMoves = moves.Count(move => !IsCorrectRubiksCubeMove(move, cubeDimension));

        if (incorrectMoves == 0) return;

        ThrowRubiksCubeMovesException(incorrectMoves);
    }

    private bool IsCorrectRubiksCubeMove(RubiksCubeMoveBase move, int cubeDimension)
    {
        var correct = move switch
        {
            RubiksCubeSliceMove cubeSliceMove => cubeMoveChecker.IsCorrectRubiksCubeSliceMove(cubeSliceMove, cubeDimension),
            WholeRubiksCubeMove wholeCubeMove => cubeMoveChecker.IsCorrectWholeRubiksCubeMove(wholeCubeMove),
            _ => false,
        };

        return correct;
    }

    private static void ThrowRubiksCubeMoveException(RubiksCubeMoveBase move)
    {
        switch (move)
        {
            case RubiksCubeSliceMove:
                ThrowRubiksCubeSliceMoveException();
                break;

            case WholeRubiksCubeMove:
                ThrowExceptionWholeRubiksCubeMoveException();
                break;

            default:
                ThrowUnsupportedRubiksCubeMoveException(move);
                break;
        }
    }

    private static void ThrowRubiksCubeSliceMoveException()
    {
        const string message = "Rubik's cube slice move is not correct. " +
                               "All of face, direction, and slice must be within valid ranges.";

        throw new ArgumentException(message);
    }

    private static void ThrowExceptionWholeRubiksCubeMoveException()
    {
        const string message = "Whole Rubik's cube move is not correct. " +
                               "All of face and direction must be within valid ranges.";

        throw new ArgumentException(message);
    }

    private static void ThrowUnsupportedRubiksCubeMoveException(RubiksCubeMoveBase move)
    {
        var message = $"Unsupported Rubik's cube move type: '{move.GetType().Name}'. " +
                      $"Support only RubiksCubeSliceMove and WholeRubiksCubeMove.";

        throw new ArgumentException(message);
    }

    private static void ThrowRubiksCubeMovesException(int incorrectMoves)
    {
        var message = "All Rubik's cube moves must be correct. " +
                      $"'{incorrectMoves}' incorrect cube moves were found.";

        throw new ArgumentException(message);
    }
}