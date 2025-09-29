using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;

namespace RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;

public sealed record SliceMove(MoveFace Face, MoveDirection Direction, int Slice) : MoveBase;