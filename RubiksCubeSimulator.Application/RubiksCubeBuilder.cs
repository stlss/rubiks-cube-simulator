using System.Collections.Immutable;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;

namespace RubiksCubeSimulator.Application;

public sealed class RubiksCubeBuilder : IRubiksCubeBuilder
{
    public RubiksCube BuildSolvedRubiksCube(int dimension)
    {
        ThrowExceptionIfDimensionIsNotCorrect(dimension);

        var upFace = BuildSolidColorRubiksCubeFace(dimension, RubiksCubeColor.White);
        var rightFace = BuildSolidColorRubiksCubeFace(dimension, RubiksCubeColor.Blue);
        var frontFace = BuildSolidColorRubiksCubeFace(dimension, RubiksCubeColor.Red);

        var downFace = BuildSolidColorRubiksCubeFace(dimension, RubiksCubeColor.Yellow);
        var leftFace = BuildSolidColorRubiksCubeFace(dimension, RubiksCubeColor.Green);
        var backFace = BuildSolidColorRubiksCubeFace(dimension, RubiksCubeColor.Orange);

        var cube = new RubiksCube(dimension, upFace, rightFace, frontFace, downFace, leftFace, backFace);
        return cube;
    }
    private static void ThrowExceptionIfDimensionIsNotCorrect(int dimension)
    {
        if (dimension >= 2) return;

        const string paramName = nameof(dimension);
        var message = $"Rubik's cube dimension must be greater or equal '2'. Actual dimension: '{dimension}'.";

        throw new ArgumentOutOfRangeException(paramName, message);
    }

    private static RubiksCubeFace BuildSolidColorRubiksCubeFace(int dimension, RubiksCubeColor color)
    {
        var stickerColors = Enumerable
            .Repeat(
                Enumerable.Repeat(color, dimension).ToImmutableArray(),
                dimension)
            .ToImmutableArray();

        var face = new RubiksCubeFace(stickerColors);
        return face;
    }
}