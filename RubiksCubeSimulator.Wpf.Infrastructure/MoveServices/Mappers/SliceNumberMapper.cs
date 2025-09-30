using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;

namespace RubiksCubeSimulator.Wpf.Infrastructure.MoveServices.Mappers;

internal interface ISliceNumberMapper
{
    public int Map(int cubeDimension, FaceName faceName, int sliceNumber);
}

internal class SliceNumberMapper : ISliceNumberMapper
{
    public int Map(int cubeDimension, FaceName faceName, int sliceNumber)
    {
        return faceName switch
        {
            FaceName.Up => sliceNumber,
            FaceName.Right => cubeDimension - sliceNumber - 1,
            FaceName.Front => sliceNumber,
            FaceName.Down => cubeDimension - sliceNumber - 1,
            FaceName.Left => sliceNumber,
            FaceName.Back => cubeDimension - sliceNumber - 1,
            _ => throw new ArgumentOutOfRangeException(nameof(faceName), faceName, null),
        };
    }
}