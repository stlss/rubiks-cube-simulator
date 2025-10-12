using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;

namespace RubiksCubeSimulator.Application.MoveGenerator;

internal sealed class MoveGenerator : IMoveGenerator
{
    private readonly Random _random = new();
    private readonly FaceName[] _faceNames = [FaceName.Up, FaceName.Right, FaceName.Front];

    public IReadOnlyList<MoveBase> Generate(int cubeDimension)
    {
        return Enumerable.Range(0, 50)
            .Select(_ => new SliceMove(
                FaceName: GetRandomFaceName(),
                Direction: MoveDirection.Clockwise,
                Slice: GetRandomSliceNumber(cubeDimension)))
            .ToList();

    }

    private int GetRandomSliceNumber(int cubeDimension) => _random.Next(0, cubeDimension);

    private FaceName GetRandomFaceName() => _faceNames[_random.Next(0, _faceNames.Length)];
}