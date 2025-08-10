using System.Collections.Immutable;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;

namespace RubiksCubeSimulator.Application.RubiksCubeBuilder;

public sealed class RubiksCubeBuilder : IRubiksCubeBuilder
{
    public RubiksCube BuildSolvedRubiksCube(int cubeDimension)
    {
        ThrowExceptionIfDimensionIsNotCorrect(cubeDimension);

        var upFace = BuildSolidColorRubiksCubeFace(cubeDimension, RubiksCubeColor.White);
        var rightFace = BuildSolidColorRubiksCubeFace(cubeDimension, RubiksCubeColor.Blue);
        var frontFace = BuildSolidColorRubiksCubeFace(cubeDimension, RubiksCubeColor.Red);

        var downFace = BuildSolidColorRubiksCubeFace(cubeDimension, RubiksCubeColor.Yellow);
        var leftFace = BuildSolidColorRubiksCubeFace(cubeDimension, RubiksCubeColor.Green);
        var backFace = BuildSolidColorRubiksCubeFace(cubeDimension, RubiksCubeColor.Orange);

        var cube = new RubiksCube(cubeDimension, upFace, rightFace, frontFace, downFace, leftFace, backFace);
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