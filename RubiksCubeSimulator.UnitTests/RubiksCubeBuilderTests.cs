using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Application;
using RubiksCubeSimulator.Application.Infrastructure.Extensions;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube;

namespace RubiksCubeSimulator.UnitTests;

[TestFixture]
public class RubiksCubeBuilderTests
{
    private IRubiksCubeBuilder _builder = null!;

    [SetUp]
    public void SetUp()
    {
        var provider = new ServiceCollection().AddRubiksCubeBuilder().BuildServiceProvider();
        _builder = provider.GetRequiredService<IRubiksCubeBuilder>();
    }

    [Test]
    public void Build_When_DimensionIsCorrect_Should_Ok()
    {
        const int cubeDimension = 5;

        var cube = _builder.BuildSolvedRubiksCube(cubeDimension);

        var faces = new List<RubiksCubeFace>
        {
            cube.UpFace, cube.RightFace, cube.FrontFace,
            cube.DownFace, cube.LeftFace, cube.BackFace,
        };

        var colors = new List<RubiksCubeStickerColor>
        {
            RubiksCubeStickerColor.White, RubiksCubeStickerColor.Blue, RubiksCubeStickerColor.Red,
            RubiksCubeStickerColor.Yellow, RubiksCubeStickerColor.Green, RubiksCubeStickerColor.Orange,
        };

        var facesWithColors = faces.Zip(colors);

        Assert.That(cube.Dimension, Is.EqualTo(cubeDimension));

        Assert.Multiple(() =>
        {
            foreach (var face in faces)
            {
                Assert.That(face.StickerColors, Has.Length.EqualTo(cubeDimension));
                Assert.That(face.StickerColors, Is.All.Length.EqualTo(cubeDimension));
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
        const int cubeDimension = -3;

        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _builder.BuildSolvedRubiksCube(cubeDimension));

        const string expectedParamName = nameof(cubeDimension);

        var expectedMessage = $"Rubik's cube dimension must be greater or equal '2'. " +
                              $"Actual cube dimension: '{cubeDimension}'. (Parameter '{expectedParamName}')";

        Assert.Multiple(() =>
        {
            Assert.That(ex.ParamName, Is.EqualTo(expectedParamName));
            Assert.That(ex.Message, Is.EqualTo(expectedMessage));
        });
    }
}