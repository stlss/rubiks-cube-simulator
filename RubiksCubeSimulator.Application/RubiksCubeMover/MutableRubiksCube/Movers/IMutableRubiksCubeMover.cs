namespace RubiksCubeSimulator.Application.RubiksCubeMover.MutableRubiksCube.Movers;

internal interface IMutableRubiksCubeMover
{
    public void MoveUp(MutableRubiksCube cube, int sliceNumber);

    public void MoveRight(MutableRubiksCube cube, int sliceNumber);

    public void MoveFront(MutableRubiksCube cube, int sliceNumber);


    public void MoveDown(MutableRubiksCube cube, int sliceNumber);

    public void MoveLeft(MutableRubiksCube cube, int sliceNumber);

    public void MoveBack(MutableRubiksCube cube, int sliceNumber);
}