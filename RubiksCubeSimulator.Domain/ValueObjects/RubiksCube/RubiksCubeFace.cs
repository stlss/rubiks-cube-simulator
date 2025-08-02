using System.Collections.Immutable;

namespace RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;

public sealed record RubiksCubeFace(ImmutableArray<ImmutableArray<RubiksCubeColor>> StickerColors);