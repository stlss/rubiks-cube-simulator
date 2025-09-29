using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;

namespace RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;

public sealed record WholeMove(AxisName Axis, MoveDirection Direction) : MoveBase;