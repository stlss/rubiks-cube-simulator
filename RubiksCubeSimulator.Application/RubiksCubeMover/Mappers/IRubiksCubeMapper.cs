using System.Collections.Immutable;
using RubiksCubeSimulator.Application.RubiksCubeMover.MutableRubiksCube;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;

namespace RubiksCubeSimulator.Application.RubiksCubeMover.Mappers;

public interface IRubiksCubeMapper
{
    public MutableRubiksCube.MutableRubiksCube Map(RubiksCube cube);

    public RubiksCube Map(MutableRubiksCube.MutableRubiksCube mutableCube);
}

internal sealed class MutableRubiksCubeMapper : IRubiksCubeMapper
{
    public MutableRubiksCube.MutableRubiksCube Map(RubiksCube cube)
    {
        var cubeDimension = cube.Dimension;

        var mutableUpFace = MapFace(cube.UpFace);
        var mutableRightFace = MapFace(cube.RightFace);
        var mutableFrontFace = MapFace(cube.FrontFace);

        var mutableDownFace = MapFace(cube.DownFace);
        var mutableLeftFace = MapFace(cube.LeftFace);
        var mutableBackFace = MapFace(cube.BackFace);

        var mutableCube = new MutableRubiksCube.MutableRubiksCube(cubeDimension,
            mutableUpFace, mutableRightFace, mutableFrontFace,
            mutableDownFace, mutableLeftFace, mutableBackFace);

        return mutableCube;

        MutableRubiksCubeFace MapFace(RubiksCubeFace face)
        {
            var stickerColors = new RubiksCubeStickerColor[cubeDimension, cubeDimension];

            for (var i = 0; i < cubeDimension; i++)
            {
                for (var j = 0; j < cubeDimension; j++)
                {
                    stickerColors[i, j] = face.StickerColors[i][j];
                }
            }

            var mutableFace = new MutableRubiksCubeFace(stickerColors);
            return mutableFace;
        }
    }

    public RubiksCube Map(MutableRubiksCube.MutableRubiksCube mutableCube)
    {
        var cubeDimension = mutableCube.Dimension;

        var upFace = MapFace(mutableCube.UpFace);
        var rightFace = MapFace(mutableCube.RightFace);
        var frontFace = MapFace(mutableCube.FrontFace);

        var downFace = MapFace(mutableCube.DownFace);
        var leftFace = MapFace(mutableCube.LeftFace);
        var backFace = MapFace(mutableCube.BackFace);

        var cube = new RubiksCube(cubeDimension, upFace, rightFace, frontFace, downFace, leftFace, backFace);
        return cube;

        RubiksCubeFace MapFace(MutableRubiksCubeFace face)
        {
            var stickerColors = new List<ImmutableArray<RubiksCubeStickerColor>>(cubeDimension);

            for (var i = 0; i < cubeDimension; i++)
            {
                var row = new List<RubiksCubeStickerColor>(cubeDimension);
                for (var j = 0; j < cubeDimension; j++) row.Add(face.StickerColors[i, j]);

                stickerColors.Add([..row]);
            }

            var mutableFace = new RubiksCubeFace([..stickerColors]);
            return mutableFace;
        }
    }
}