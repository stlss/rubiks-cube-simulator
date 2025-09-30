using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;

namespace RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;

public sealed record SliceMove(FaceName FaceName, MoveDirection Direction, int Slice) : MoveBase;