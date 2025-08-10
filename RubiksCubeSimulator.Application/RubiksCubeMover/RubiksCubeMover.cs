using RubiksCubeSimulator.Application.RubiksCubeMover.Mappers;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;

namespace RubiksCubeSimulator.Application.RubiksCubeMover;

public sealed class RubiksCubeMover(
    IRubiksCubeMoveExceptionThrower cubeMoveExceptionThrower,
    IRubiksCubeMapper cubeMapper,
    IRubiksCubeMoveMapper cubeMoveMapper)
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
        throw new NotImplementedException();
    }
}