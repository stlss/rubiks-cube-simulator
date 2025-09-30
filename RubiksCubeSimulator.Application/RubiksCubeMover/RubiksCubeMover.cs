using RubiksCubeSimulator.Application.RubiksCubeMover.Mappers;
using RubiksCubeSimulator.Application.RubiksCubeMover.MutableRubiksCube.Movers;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;

namespace RubiksCubeSimulator.Application.RubiksCubeMover;

internal sealed class RubiksCubeMover(
    IRubiksCubeMoveExceptionThrower cubeMoveExceptionThrower,
    IRubiksCubeMapper cubeMapper,
    IRubiksCubeMoveMapper cubeMoveMapper,
    IClockwiseMutableRubiksCubeMover clockwiseMutableCubeMover,
    ICounterclockwiseMutableRubiksCubeMover counterclockwiseMutableCubeMover)
    : IRubiksCubeMover
{
    public RubiksCube Move(RubiksCube cube, MoveBase move)
    {
        cubeMoveExceptionThrower.ThrowExceptionIfRubiksCubeIsNotCorrect(cube);
        cubeMoveExceptionThrower.ThrowExceptionIfRubiksCubeMoveIsNotCorrect(move, cube.Dimension);

        var mutableCube = cubeMapper.Map(cube);
        Move(mutableCube, move);

        var movedCube = cubeMapper.Map(mutableCube);
        return movedCube;
    }

    public RubiksCube Move(RubiksCube cube, IEnumerable<MoveBase> moves)
    {
        var moveArray = moves.ToArray();

        cubeMoveExceptionThrower.ThrowExceptionIfRubiksCubeIsNotCorrect(cube);
        cubeMoveExceptionThrower.ThrowExceptionIfRubiksCubeMovesIsNotCorrect(moveArray, cube.Dimension);

        var mutableCube = cubeMapper.Map(cube);
        foreach (var move in moveArray) Move(mutableCube, move);

        var movedCube = cubeMapper.Map(mutableCube);
        return movedCube;
    }


    private void Move(MutableRubiksCube.MutableRubiksCube mutableCube, MoveBase move)
    {
        switch (move)
        {
            case WholeMove wholeCubeMove:
                Move(mutableCube, wholeCubeMove);
                break;

            case SliceMove cubeSliceMove:
                Move(mutableCube, cubeSliceMove);
                break;
        }
    }

    private void Move(MutableRubiksCube.MutableRubiksCube mutableCube, WholeMove move)
    {
        var sliceMoves = cubeMoveMapper.Map(move, mutableCube.Dimension);
        foreach (var sliceMove in sliceMoves) Move(mutableCube, sliceMove);
    }

    private void Move(MutableRubiksCube.MutableRubiksCube mutableCube, SliceMove move)
    {
        switch (move.Direction)
        {
            case MoveDirection.Clockwise:
                MoveClockwise(mutableCube, move.FaceName, move.Slice);
                break;

            case MoveDirection.Counterclockwise:
                MoveCounterclockwise(mutableCube, move.FaceName, move.Slice);
                break;
        }
    }


    private void MoveClockwise(MutableRubiksCube.MutableRubiksCube mutableCube,
        FaceName faceName, int sliceNumber)
    {
        var clockwiseMove = _clockwiseMoveByFaceMove[faceName];
        clockwiseMove(mutableCube, sliceNumber);
    }

    private readonly Dictionary<FaceName, Action<MutableRubiksCube.MutableRubiksCube, int>>
        _clockwiseMoveByFaceMove = new()
        {
            [FaceName.Up] = clockwiseMutableCubeMover.MoveUp,
            [FaceName.Right] = clockwiseMutableCubeMover.MoveRight,
            [FaceName.Front] = clockwiseMutableCubeMover.MoveFront,
            [FaceName.Down] = clockwiseMutableCubeMover.MoveDown,
            [FaceName.Left] = clockwiseMutableCubeMover.MoveLeft,
            [FaceName.Back] = clockwiseMutableCubeMover.MoveBack,
        };


    private void MoveCounterclockwise(
        MutableRubiksCube.MutableRubiksCube mutableCube,
        FaceName faceName,
        int sliceNumber)
    {
        var counterclockwiseMove = _counterclockwiseMoveByFaceMove[faceName];
        counterclockwiseMove(mutableCube, sliceNumber);
    }

    private readonly Dictionary<FaceName, Action<MutableRubiksCube.MutableRubiksCube, int>>
        _counterclockwiseMoveByFaceMove = new()
        {
            [FaceName.Up] = counterclockwiseMutableCubeMover.MoveUp,
            [FaceName.Right] = counterclockwiseMutableCubeMover.MoveRight,
            [FaceName.Front] = counterclockwiseMutableCubeMover.MoveFront,
            [FaceName.Down] = counterclockwiseMutableCubeMover.MoveDown,
            [FaceName.Left] = counterclockwiseMutableCubeMover.MoveLeft,
            [FaceName.Back] = counterclockwiseMutableCubeMover.MoveBack,
        };
}