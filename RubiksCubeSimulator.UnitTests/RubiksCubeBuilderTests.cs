using RubiksCubeSimulator.Application;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;

namespace RubiksCubeSimulator.UnitTests;

[TestFixture]
public class RubiksCubeBuilderTests
{
    private RubiksCubeBuilder _builder = null!;

    [SetUp]
    public void SetUp()
    {
        _builder = new RubiksCubeBuilder();
    }

    [Test]
    public void Build_When_DimensionIsCorrect_Should_Ok()
    {
        const int dimension = 5;

        var cube = _builder.BuildSolvedRubiksCube(dimension);

        var faces = new List<RubiksCubeFace>
        {
            cube.UpFace, cube.RightFace, cube.FrontFace,
            cube.DownFace, cube.LeftFace, cube.BackFace,
        };

        var colors = new List<RubiksCubeColor>
        {
            RubiksCubeColor.White, RubiksCubeColor.Blue, RubiksCubeColor.Red,
            RubiksCubeColor.Yellow, RubiksCubeColor.Green, RubiksCubeColor.Orange,
        };

        var facesWithColors = faces.Zip(colors);

        Assert.That(cube.Dimension, Is.EqualTo(dimension));

        Assert.Multiple(() =>
        {
            foreach (var face in faces)
            {
                Assert.That(face.StickerColors, Has.Length.EqualTo(dimension));
                Assert.That(face.StickerColors, Is.All.Length.EqualTo(dimension));
            }
        });

        Assert.Multiple(() =>
        {
            foreach (var (face, color) in facesWithColors)
            {
                var stickerColors = face.StickerColors.SelectMany(row => row);
                Assert.That(stickerColors, Is.All.EqualTo(color));
            }
        });
    }

    [Test]
    public void Build_When_DimensionIsNotCorrect_Should_Error()
    {
        const int dimension = -3;

        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _builder.BuildSolvedRubiksCube(dimension));

        const string expectedParamName = nameof(dimension);
        var expectedMessage = $"Rubik's cube dimension must be greater or equal '2'. Actual dimension: '{dimension}'. (Parameter 'dimension')";

        Assert.Multiple(() =>
        {
            Assert.That(ex.ParamName, Is.EqualTo(expectedParamName));
            Assert.That(ex.Message, Is.EqualTo(expectedMessage));
        });
    }
}