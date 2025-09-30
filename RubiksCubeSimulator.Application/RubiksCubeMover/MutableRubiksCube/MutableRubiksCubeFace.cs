using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;

namespace RubiksCubeSimulator.Application.RubiksCubeMover.MutableRubiksCube;

internal sealed record MutableRubiksCubeFace(RubiksCubeStickerColor[,] StickerColors);