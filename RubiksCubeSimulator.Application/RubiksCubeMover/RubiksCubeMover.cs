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
                MoveClockwise(mutableCube, move.Face, move.Slice);
                break;

            case MoveDirection.Counterclockwise:
                MoveCounterclockwise(mutableCube, move.Face, move.Slice);
                break;
        }
    }


    private void MoveClockwise(MutableRubiksCube.MutableRubiksCube mutableCube,
        MoveFace moveFace, int sliceNumber)
    {
        var clockwiseMove = _clockwiseMoveByFaceMove[moveFace];
        clockwiseMove(mutableCube, sliceNumber);
    }

    private readonly Dictionary<MoveFace, Action<MutableRubiksCube.MutableRubiksCube, int>>
        _clockwiseMoveByFaceMove = new()
        {
            [MoveFace.Up] = clockwiseMutableCubeMover.MoveUp,
            [MoveFace.Right] = clockwiseMutableCubeMover.MoveRight,
            [MoveFace.Front] = clockwiseMutableCubeMover.MoveFront,
            [MoveFace.Down] = clockwiseMutableCubeMover.MoveDown,
            [MoveFace.Left] = clockwiseMutableCubeMover.MoveLeft,
            [MoveFace.Back] = clockwiseMutableCubeMover.MoveBack,
        };


    private void MoveCounterclockwise(
        MutableRubiksCube.MutableRubiksCube mutableCube,
        MoveFace moveFace,
        int sliceNumber)
    {
        var counterclockwiseMove = _counterclockwiseMoveByFaceMove[moveFace];
        counterclockwiseMove(mutableCube, sliceNumber);
    }

    private readonly Dictionary<MoveFace, Action<MutableRubiksCube.MutableRubiksCube, int>>
        _counterclockwiseMoveByFaceMove = new()
        {
            [MoveFace.Up] = counterclockwiseMutableCubeMover.MoveUp,
            [MoveFace.Right] = counterclockwiseMutableCubeMover.MoveRight,
            [MoveFace.Front] = counterclockwiseMutableCubeMover.MoveFront,
            [MoveFace.Down] = counterclockwiseMutableCubeMover.MoveDown,
            [MoveFace.Left] = counterclockwiseMutableCubeMover.MoveLeft,
            [MoveFace.Back] = counterclockwiseMutableCubeMover.MoveBack,
        };
}