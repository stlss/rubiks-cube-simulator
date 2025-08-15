namespace RubiksCubeSimulator.Application.Infrastructure.Extensions;

internal static class MatrixExtensions
{
    public static void ClockwiseRotateSquareMatrix<T>(this T[,] matrix)
    {
        var copyMatrix = matrix.CopySquareMatrix();
        var sizeSquare = matrix.GetLength(0);

        for (var i = 0; i < sizeSquare; i++)
        {
            for (var j = 0; j < sizeSquare; j++)
            {
                matrix[j, sizeSquare - i - 1] = copyMatrix[i, j];
            }
        }
    }

    public static void CounterclockwiseRotateSquareMatrix<T>(this T[,] matrix)
    {
        var copyMatrix = matrix.CopySquareMatrix();
        var sizeSquare = matrix.GetLength(0);

        for (var i = 0; i < sizeSquare; i++)
        {
            for (var j = 0; j < sizeSquare; j++)
            {
                matrix[sizeSquare - j - 1, i] = copyMatrix[i, j];
            }
        }
    }

    private static T[,] CopySquareMatrix<T>(this T[,] matrix)
    {
        var squareSize =  matrix.GetLength(0);
        var copyMatrix = new T[squareSize, squareSize];

        Array.Copy(matrix, copyMatrix, matrix.Length);
        return copyMatrix;
    }
}