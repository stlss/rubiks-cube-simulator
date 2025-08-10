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
        var movedMutableCube = MoveMutableRubiksCube(mutableCube, move);
        var movedCube = cubeMapper.Map(movedMutableCube);

        return movedCube;
    }

    public RubiksCube MoveRubiksCube(RubiksCube cube, IEnumerable<RubiksCubeMoveBase> moves)
    {
        var moveArray = moves.ToArray();

        cubeMoveExceptionThrower.ThrowExceptionIfRubiksCubeIsNotCorrect(cube);
        cubeMoveExceptionThrower.ThrowExceptionIfRubiksCubeMovesIsNotCorrect(moveArray, cube.Dimension);

        var mutableCube = cubeMapper.Map(cube);
        var movedMutableCube = moveArray.Aggregate(mutableCube, MoveMutableRubiksCube);
        var movedCube = cubeMapper.Map(movedMutableCube);

        return movedCube;
    }

    private MutableRubiksCube.MutableRubiksCube MoveMutableRubiksCube(
        MutableRubiksCube.MutableRubiksCube cube, RubiksCubeMoveBase move)
    {
        var movedCube = move switch
        {
            WholeRubiksCubeMove wholeCubeMove => MoveMutableRubiksCube(cube, wholeCubeMove),
            RubiksCubeSliceMove cubeSliceMove => MoveMutableRubiksCube(cube, cubeSliceMove),
            _ => cube,
        };

        return movedCube;
    }

    private MutableRubiksCube.MutableRubiksCube MoveMutableRubiksCube(
        MutableRubiksCube.MutableRubiksCube cube, WholeRubiksCubeMove move)
    {
        var moves = cubeMoveMapper.Map(move, cube.Dimension);
        var movedCube = moves.Aggregate(cube, MoveMutableRubiksCube);

        return movedCube;
    }

    private MutableRubiksCube.MutableRubiksCube MoveMutableRubiksCube(
        MutableRubiksCube.MutableRubiksCube cube, RubiksCubeSliceMove move)
    {
        var movedCube = move.Direction switch
        {
            MoveDirection.Clockwise => MoveMutableRubiksCubeClockwise(cube, move.Face, move.Slice),
            MoveDirection.CounterClockwise => MoveMutableRubiksCubeCounterclockwise(cube, move.Face, move.Slice),
            _ => cube,
        };

        return movedCube;
    }

    private MutableRubiksCube.MutableRubiksCube MoveMutableRubiksCubeClockwise(
        MutableRubiksCube.MutableRubiksCube cube, MoveFace moveFace, int sliceNumber)
    {
        var movedCube = moveFace switch
        {
            MoveFace.Up => clockwiseMutableCubeMover.MoveMutableRubiksCubeUp(cube, sliceNumber),
            MoveFace.Right => clockwiseMutableCubeMover.MoveMutableRubiksCubeRight(cube, sliceNumber),
            MoveFace.Front => clockwiseMutableCubeMover.MoveMutableRubiksCubeFront(cube, sliceNumber),
            MoveFace.Down => clockwiseMutableCubeMover.MoveMutableRubiksCubeDown(cube, sliceNumber),
            MoveFace.Left => clockwiseMutableCubeMover.MoveMutableRubiksCubeLeft(cube, sliceNumber),
            MoveFace.Back => clockwiseMutableCubeMover.MoveMutableRubiksCubeBack(cube, sliceNumber),
            _ => cube,
        };

        return movedCube;
    }

    private MutableRubiksCube.MutableRubiksCube MoveMutableRubiksCubeCounterclockwise(
        MutableRubiksCube.MutableRubiksCube cube, MoveFace moveFace, int sliceNumber)
    {
        var movedCube = moveFace switch
        {
            MoveFace.Up => counterclockwiseMutableCubeMover.MoveMutableRubiksCubeUp(cube, sliceNumber),
            MoveFace.Right => counterclockwiseMutableCubeMover.MoveMutableRubiksCubeRight(cube, sliceNumber),
            MoveFace.Front => counterclockwiseMutableCubeMover.MoveMutableRubiksCubeFront(cube, sliceNumber),
            MoveFace.Down => counterclockwiseMutableCubeMover.MoveMutableRubiksCubeDown(cube, sliceNumber),
            MoveFace.Left => counterclockwiseMutableCubeMover.MoveMutableRubiksCubeLeft(cube, sliceNumber),
            MoveFace.Back => counterclockwiseMutableCubeMover.MoveMutableRubiksCubeBack(cube, sliceNumber),
            _ => cube,
        };

        return movedCube;
    }
}