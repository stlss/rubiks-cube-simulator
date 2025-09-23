namespace RubiksCubeSimulator.Application.RubiksCubeBuilder;

internal interface IRubiksCubeBuildExceptionThrower
{
    public void ThrowExceptionIfRubiksCubeDimensionIsNotCorrect(int cubeDimension);
}

internal sealed class RubiksCubeBuildExceptionThrower  : IRubiksCubeBuildExceptionThrower
{
    public void ThrowExceptionIfRubiksCubeDimensionIsNotCorrect(int cubeDimension)
    {
        if (cubeDimension >= 2) return;

        const string paramName = nameof(cubeDimension);

        var message = $"Rubik's cube dimension must be greater or equal '2'. " +
                      $"Actual cube dimension: '{cubeDimension}'.";

        throw new ArgumentOutOfRangeException(paramName, message);
    }
}