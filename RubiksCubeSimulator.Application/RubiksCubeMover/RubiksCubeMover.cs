using RubiksCubeSimulator.Application.RubiksCubeMover.Mappers;
using RubiksCubeSimulator.Application.RubiksCubeMover.MutableRubiksCube.Movers;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;

namespace RubiksCubeSimulator.Application.RubiksCubeMover;

public sealed class RubiksCubeMover(
    IRubiksCubeMoveExceptionThrower cubeMoveExceptionThrower,
    IRubiksCubeMapper cubeMapper,
    IRubiksCubeMoveMapper cubeMoveMapper,
    IClockwiseMutableRubiksCubeMover clockwiseMutableCubeMover,
    ICounterclockwiseMutableRubiksCubeMover counterclockwiseMutableCubeMover)
    : IRubiksCubeMover
{
    public RubiksCube MoveRubiksCube(RubiksCube cube, RubiksCubeMoveBase move)
    {
        cubeMoveExceptionThrower.ThrowExceptionIfRubiksCubeIsNotCorrect(cube);
        cubeMoveExceptionThrower.ThrowExceptionIfRubiksCubeMoveIsNotCorrect(move, cube.Dimension);

        var mutableCube = cubeMapper.Map(cube);
        MoveMutableRubiksCube(mutableCube, move);

        var movedCube = cubeMapper.Map(mutableCube);
        return movedCube;
    }

    public RubiksCube MoveRubiksCube(RubiksCube cube, IEnumerable<RubiksCubeMoveBase> moves)
    {
        var moveArray = moves.ToArray();

        cubeMoveExceptionThrower.ThrowExceptionIfRubiksCubeIsNotCorrect(cube);
        cubeMoveExceptionThrower.ThrowExceptionIfRubiksCubeMovesIsNotCorrect(moveArray, cube.Dimension);

        var mutableCube = cubeMapper.Map(cube);
        foreach (var move in moveArray) MoveMutableRubiksCube(mutableCube, move);

        var movedCube = cubeMapper.Map(mutableCube);
        return movedCube;
    }


    private void MoveMutableRubiksCube(MutableRubiksCube.MutableRubiksCube cube, RubiksCubeMoveBase move)
    {
        switch (move)
        {
            case WholeRubiksCubeMove wholeCubeMove:
                MoveMutableRubiksCube(cube, wholeCubeMove);
                break;

            case RubiksCubeSliceMove cubeSliceMove:
                MoveMutableRubiksCube(cube, cubeSliceMove);
                break;
        }
    }

    private void MoveMutableRubiksCube(MutableRubiksCube.MutableRubiksCube cube, WholeRubiksCubeMove move)
    {
        var sliceMoves = cubeMoveMapper.Map(move, cube.Dimension);
        foreach (var sliceMove in sliceMoves) MoveMutableRubiksCube(cube, sliceMove);
    }

    private void MoveMutableRubiksCube(MutableRubiksCube.MutableRubiksCube cube, RubiksCubeSliceMove move)
    {
        switch (move.Direction)
        {
            case MoveDirection.Clockwise:
                MoveMutableRubiksCubeClockwise(cube, move.Face, move.Slice);
                break;

            case MoveDirection.CounterClockwise:
                MoveMutableRubiksCubeCounterclockwise(cube, move.Face, move.Slice);
                break;
        }
    }


    private void MoveMutableRubiksCubeClockwise(MutableRubiksCube.MutableRubiksCube cube,
        MoveFace moveFace, int sliceNumber)
    {
        var clockwiseMove = _clockwiseMoveByFaceMove[moveFace];
        clockwiseMove(cube, sliceNumber);
    }

    private readonly Dictionary<MoveFace, Action<MutableRubiksCube.MutableRubiksCube, int>>
        _clockwiseMoveByFaceMove = new()
        {
            [MoveFace.Up] = clockwiseMutableCubeMover.MoveMutableRubiksCubeUp,
            [MoveFace.Right] = clockwiseMutableCubeMover.MoveMutableRubiksCubeRight,
            [MoveFace.Front] = clockwiseMutableCubeMover.MoveMutableRubiksCubeFront,
            [MoveFace.Down] = clockwiseMutableCubeMover.MoveMutableRubiksCubeDown,
            [MoveFace.Left] = clockwiseMutableCubeMover.MoveMutableRubiksCubeLeft,
            [MoveFace.Back] = clockwiseMutableCubeMover.MoveMutableRubiksCubeBack,
        };


    private void MoveMutableRubiksCubeCounterclockwise(MutableRubiksCube.MutableRubiksCube cube, MoveFace moveFace,
        int sliceNumber)
    {
        var counterclockwiseMove = _counterclockwiseMoveByFaceMove[moveFace];
        counterclockwiseMove(cube, sliceNumber);
    }

    private readonly Dictionary<MoveFace, Action<MutableRubiksCube.MutableRubiksCube, int>>
        _counterclockwiseMoveByFaceMove = new()
        {
            [MoveFace.Up] = counterclockwiseMutableCubeMover.MoveMutableRubiksCubeUp,
            [MoveFace.Right] = counterclockwiseMutableCubeMover.MoveMutableRubiksCubeRight,
            [MoveFace.Front] = counterclockwiseMutableCubeMover.MoveMutableRubiksCubeFront,
            [MoveFace.Down] = counterclockwiseMutableCubeMover.MoveMutableRubiksCubeDown,
            [MoveFace.Left] = counterclockwiseMutableCubeMover.MoveMutableRubiksCubeLeft,
            [MoveFace.Back] = counterclockwiseMutableCubeMover.MoveMutableRubiksCubeBack,
        };
}