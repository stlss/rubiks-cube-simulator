using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;

namespace RubiksCubeSimulator.Application.RubiksCubeMover.Checkers;

public interface IRubiksCubeChecker
{
    public bool IsCorrectRubiksCube(RubiksCube cube);
}

internal sealed class RubiksCubeChecker : IRubiksCubeChecker
{
    public bool IsCorrectRubiksCube(RubiksCube cube)
    {
        if (cube.Dimension < 2) return false;

        var faces = new List<RubiksCubeFace>
        {
            cube.UpFace, cube.RightFace, cube.FrontFace,
            cube.DownFace, cube.LeftFace, cube.BackFace
        };

        var isCorrectAllFaceDimensions = faces.All(face => IsCorrectRubiksCubeFace(face, cube.Dimension));
        return isCorrectAllFaceDimensions;
    }

    private static bool IsCorrectRubiksCubeFace(RubiksCubeFace face, int cubeDimension)
    {
        if (face.StickerColors.Length != cubeDimension) return false;

        for (var i = 0; i < cubeDimension; i++)
        {
            if (face.StickerColors[i].Length != cubeDimension) return false;
        }

        return true;
    }
}