using System.Collections.Immutable;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;

namespace RubiksCubeSimulator.UnitTests.Infrastructure.RubiksCubeComparers;

internal sealed class RubiksCubeFaceEqualityComparer : IEqualityComparer<RubiksCubeFace>
{
    public bool Equals(RubiksCubeFace? x, RubiksCubeFace? y)
    {
        if (ReferenceEquals(x, y)) return true;

        if (x is null) return false;
        if (y is null) return false;

        return EqualStickerColors(x.StickerColors, y.StickerColors);
    }

    private static bool EqualStickerColors(ImmutableArray<ImmutableArray<RubiksCubeStickerColor>> colors1,
        ImmutableArray<ImmutableArray<RubiksCubeStickerColor>> colors2)
    {
        if (colors1.Length != colors2.Length) return false;

        for (var i = 0; i < colors1.Length; i++)
        {
            if (colors1[i].Length != colors2[i].Length) return false;

            for (var j = 0; j < colors1[i].Length; j++)
            {
                if (colors1[i][j] != colors2[i][j]) return false;
            }
        }

        return true;
    }

    public int GetHashCode(RubiksCubeFace obj)
    {
        var hash = new HashCode();
        foreach (var color in obj.StickerColors.SelectMany(row => row)) hash.Add(color);
        return hash.ToHashCode();
    }
}