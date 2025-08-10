using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;

namespace RubiksCubeSimulator.Application.RubiksCubeMover.MutableRubiksCube;

public sealed record MutableRubiksCubeFace(RubiksCubeStickerColor[,] StickerColors);