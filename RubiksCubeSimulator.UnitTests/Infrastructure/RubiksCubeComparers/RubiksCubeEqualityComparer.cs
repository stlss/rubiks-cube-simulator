using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;

namespace RubiksCubeSimulator.UnitTests.Infrastructure.RubiksCubeComparers;

internal sealed class RubiksCubeEqualityComparer : IEqualityComparer<RubiksCube>
{
    private readonly RubiksCubeFaceEqualityComparer _faceEqualityEqualityComparer = new();

    public bool Equals(RubiksCube? x, RubiksCube? y)
    {
        if (ReferenceEquals(x, y)) return true;

        if (x is null) return false;
        if (y is null) return false;

        return x.Dimension == y.Dimension
               && _faceEqualityEqualityComparer.Equals(x.UpFace, y.UpFace)
               && _faceEqualityEqualityComparer.Equals(x.RightFace, y.RightFace)
               && _faceEqualityEqualityComparer.Equals(x.FrontFace, y.FrontFace)
               && _faceEqualityEqualityComparer.Equals(x.DownFace, y.DownFace)
               && _faceEqualityEqualityComparer.Equals(x.LeftFace, y.LeftFace)
               && _faceEqualityEqualityComparer.Equals(x.BackFace, y.BackFace);
    }

    public int GetHashCode(RubiksCube obj)
    {
        var hash = new HashCode();

        hash.Add(obj.Dimension);

        hash.Add(obj.UpFace.GetHashCode());
        hash.Add(obj.RightFace.GetHashCode());
        hash.Add(obj.FrontFace.GetHashCode());

        hash.Add(obj.DownFace.GetHashCode());
        hash.Add(obj.LeftFace.GetHashCode());
        hash.Add(obj.BackFace.GetHashCode());

        return hash.ToHashCode();
    }
}