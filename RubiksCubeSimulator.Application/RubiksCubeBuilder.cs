using System.Collections.Immutable;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;

namespace RubiksCubeSimulator.Application;

public sealed class RubiksCubeBuilder : IRubiksCubeBuilder
{
    public RubiksCube BuildSolvedRubiksCube(int dimension)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(dimension, 2);

        var upFace = BuildRubiksCubeFace(RubiksCubeColor.White);
        var rightFace = BuildRubiksCubeFace(RubiksCubeColor.Blue);
        var frontFace = BuildRubiksCubeFace(RubiksCubeColor.Red);

        var downFace = BuildRubiksCubeFace(RubiksCubeColor.Yellow);
        var leftFace = BuildRubiksCubeFace(RubiksCubeColor.Green);
        var backFace = BuildRubiksCubeFace(RubiksCubeColor.Orange);

        var cube = new RubiksCube(dimension, upFace, rightFace, frontFace, downFace, leftFace, backFace);
        return cube;

        RubiksCubeFace BuildRubiksCubeFace(RubiksCubeColor color)
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
}