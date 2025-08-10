using System.Collections.Immutable;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;

namespace RubiksCubeSimulator.Application.RubiksCubeBuilder;

public sealed class RubiksCubeBuilder(IRubiksCubeBuildExceptionThrower cubeBuildExceptionThrower) : IRubiksCubeBuilder
{
    public RubiksCube BuildSolvedRubiksCube(int cubeDimension)
    {
        cubeBuildExceptionThrower.ThrowExceptionIfRubiksCubeDimensionIsNotCorrect(cubeDimension);

        var upFace = BuildSolidColorRubiksCubeFace(cubeDimension, RubiksCubeStickerColor.White);
        var rightFace = BuildSolidColorRubiksCubeFace(cubeDimension, RubiksCubeStickerColor.Blue);
        var frontFace = BuildSolidColorRubiksCubeFace(cubeDimension, RubiksCubeStickerColor.Red);

        var downFace = BuildSolidColorRubiksCubeFace(cubeDimension, RubiksCubeStickerColor.Yellow);
        var leftFace = BuildSolidColorRubiksCubeFace(cubeDimension, RubiksCubeStickerColor.Green);
        var backFace = BuildSolidColorRubiksCubeFace(cubeDimension, RubiksCubeStickerColor.Orange);

        var cube = new RubiksCube(cubeDimension, upFace, rightFace, frontFace, downFace, leftFace, backFace);
        return cube;
    }

    private static RubiksCubeFace BuildSolidColorRubiksCubeFace(int dimension, RubiksCubeStickerColor stickerColor)
    {
        var stickerColors = Enumerable
            .Repeat(
                Enumerable.Repeat(stickerColor, dimension).ToImmutableArray(),
                dimension)
            .ToImmutableArray();

        var face = new RubiksCubeFace(stickerColors);
        return face;
    }
}