using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;

namespace RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;

public sealed record RubiksCubeSliceMove(MoveFace Face, MoveDirection Direction, int Slice) : RubiksCubeMoveBase;