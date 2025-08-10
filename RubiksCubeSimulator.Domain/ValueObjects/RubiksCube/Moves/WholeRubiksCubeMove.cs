using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;

namespace RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;

public sealed record WholeRubiksCubeMove(AxisName Axis, MoveDirection Direction);