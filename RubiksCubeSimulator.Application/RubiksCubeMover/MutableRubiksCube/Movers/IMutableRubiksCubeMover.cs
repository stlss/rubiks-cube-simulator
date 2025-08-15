namespace RubiksCubeSimulator.Application.RubiksCubeMover.MutableRubiksCube.Movers;

public interface IMutableRubiksCubeMover
{
    public void MoveMutableRubiksCubeUp(MutableRubiksCube cube, int sliceNumber);

    public void MoveMutableRubiksCubeRight(MutableRubiksCube cube, int sliceNumber);

    public void MoveMutableRubiksCubeFront(MutableRubiksCube cube, int sliceNumber);


    public void MoveMutableRubiksCubeDown(MutableRubiksCube cube, int sliceNumber);

    public void MoveMutableRubiksCubeLeft(MutableRubiksCube cube, int sliceNumber);

    public void MoveMutableRubiksCubeBack(MutableRubiksCube cube, int sliceNumber);
}