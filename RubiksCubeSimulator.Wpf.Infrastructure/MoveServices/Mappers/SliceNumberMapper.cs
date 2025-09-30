using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;

namespace RubiksCubeSimulator.Wpf.Infrastructure.MoveServices.Mappers;

internal interface ISliceNumberMapper
{
    public int Map(int cubeDimension, MoveFace moveFace, int sliceNumber);
}

internal class SliceNumberMapper : ISliceNumberMapper
{
    public int Map(int cubeDimension, MoveFace moveFace, int sliceNumber)
    {
        return moveFace switch
        {
            MoveFace.Up => sliceNumber,
            MoveFace.Right => cubeDimension - sliceNumber - 1,
            MoveFace.Front => sliceNumber,
            MoveFace.Down => cubeDimension - sliceNumber - 1,
            MoveFace.Left => sliceNumber,
            MoveFace.Back => cubeDimension - sliceNumber - 1,
            _ => throw new ArgumentOutOfRangeException(nameof(moveFace), moveFace, null),
        };
    }
}