using Microsoft.Extensions.DependencyInjection;
using RubiksCubeSimulator.Application.Extensions;
using RubiksCubeSimulator.Domain.Services;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves;
using RubiksCubeSimulator.Domain.ValueObjects.RubiksCube.Moves.Enums;
using RubiksCubeSimulator.UnitTests.Infrastructure.RubiksCubeComparers;
using RubiksCubeSimulator.UnitTests.Infrastructure.RubiksCubeConverters;
using RubiksCubeSimulator.UnitTests.Infrastructure.RubiksCubeProviders;

namespace RubiksCubeSimulator.UnitTests;

[TestFixture]
public sealed class RubiksCubeMoverTests
{
    private const int StartCubeDimensionTest = 2;
    private const int EndCubeDimensionTest = 7;

    private static readonly AxisName[] AxisNames = (AxisName[])Enum.GetValues(typeof(AxisName));
    private static readonly MoveDirection[] MoveDirections = (MoveDirection[])Enum.GetValues(typeof(MoveDirection));
    private static readonly MoveFace[] MoveFaces = (MoveFace[])Enum.GetValues(typeof(MoveFace));

    private readonly RubiksCubeFaceEqualityComparer _cubeFaceEqualityComparer = new();
    private readonly RubiksCubeEqualityComparer _cubeEqualityComparer = new();

    private IRubiksCubeBuilder _builder = null!;
    private IRubiksCubeMover _mover = null!;


    [SetUp]
    public void SetUp()
    {
        var provider = new ServiceCollection()
            .AddRubiksCubeBuilder()
            .AddRubiksCubeMover()
            .BuildServiceProvider();

        _builder = provider.GetRequiredService<IRubiksCubeBuilder>();
        _mover = provider.GetRequiredService<IRubiksCubeMover>();
    }


    [TestCaseSource(nameof(Get_SliceMove_When_RepeatedFourTimesMove_Should_SolvedCube_TestCases))]
    public void SliceMove_When_RepeatedFourTimesMove_Should_SolvedCube(int cubeDimension,
        IEnumerable<MoveBase> moves)
    {
        var cube = _builder.BuildSolvedCube(cubeDimension);

        var movedCube = _mover.Move(cube, moves);

        Assert.That(_cubeEqualityComparer.Equals(cube, movedCube), Is.True);
    }

    private static IEnumerable<TestCaseData> Get_SliceMove_When_RepeatedFourTimesMove_Should_SolvedCube_TestCases()
    {
        for (var cubeDimension = StartCubeDimensionTest; cubeDimension <= EndCubeDimensionTest; cubeDimension++)
        {
            foreach (var moveFace in MoveFaces)
            {
                foreach (var moveDirection in MoveDirections)
                {
                    for (var sliceNumber = 0; sliceNumber < cubeDimension; sliceNumber++)
                    {
                        var testCase = new TestCaseData(
                            cubeDimension,
                            Enumerable.Repeat(new SliceMove(moveFace, moveDirection, sliceNumber), 4));

                        testCase.SetName($"{nameof(SliceMove_When_RepeatedFourTimesMove_Should_SolvedCube)}" +
                                         $"(cubeDimension: {cubeDimension}, moveFace: {moveFace}, " +
                                         $"moveDirection: {moveDirection}, sliceNumber: {sliceNumber})");

                        yield return testCase;
                    }
                }
            }
        }
    }


    [TestCaseSource(nameof(Get_WholeCubeMove_When_RepeatedFourTimesMove_Should_SolvedCube_TestCases))]
    public void WholeCubeMove_When_RepeatedFourTimesMove_Should_SolvedCube(int cubeDimension,
        IEnumerable<MoveBase> moves)
    {
        var cube = _builder.BuildSolvedCube(cubeDimension);

        var movedCube = _mover.Move(cube, moves);

        Assert.That(_cubeEqualityComparer.Equals(cube, movedCube), Is.True);
    }

    private static IEnumerable<TestCaseData> Get_WholeCubeMove_When_RepeatedFourTimesMove_Should_SolvedCube_TestCases()
    {
        for (var cubeDimension = StartCubeDimensionTest; cubeDimension <= EndCubeDimensionTest; cubeDimension++)
        {
            foreach (var axisName in AxisNames)
            {
                foreach (var moveDirection in MoveDirections)
                {
                    var testCase = new TestCaseData(
                        cubeDimension,
                        Enumerable.Repeat(new WholeMove(axisName, moveDirection), 4));

                    testCase.SetName($"{nameof(WholeCubeMove_When_RepeatedFourTimesMove_Should_SolvedCube)}" +
                                     $"(cubeDimension: {cubeDimension}, axisName: {axisName}, " +
                                     $"moveDirection: {moveDirection})");

                    yield return testCase;
                }
            }
        }
    }


    [TestCaseSource(nameof(Get_WholeCubeMove_Should_YellowUpOrangeRightGreenFrontCube_TestCases))]
    public void WholeCubeMove_Should_YellowUpOrangeRightGreenFrontCube(int cubeDimension)
    {
        var cube = _builder.BuildSolvedCube(cubeDimension);

        var moves = new List<MoveBase>
        {
            new WholeMove(AxisName.X, MoveDirection.Clockwise),
            new WholeMove(AxisName.X, MoveDirection.Clockwise),
            new WholeMove(AxisName.Y, MoveDirection.Counterclockwise),
        };

        var movedCube = _mover.Move(cube, moves);

        Assert.Multiple(() =>
        {
            Assert.That(_cubeFaceEqualityComparer.Equals(cube.UpFace, movedCube.DownFace), Is.True);
            Assert.That(_cubeFaceEqualityComparer.Equals(cube.RightFace, movedCube.BackFace), Is.True);
            Assert.That(_cubeFaceEqualityComparer.Equals(cube.FrontFace, movedCube.LeftFace), Is.True);

            Assert.That(_cubeFaceEqualityComparer.Equals(cube.DownFace, movedCube.UpFace), Is.True);
            Assert.That(_cubeFaceEqualityComparer.Equals(cube.BackFace, movedCube.RightFace), Is.True);
            Assert.That(_cubeFaceEqualityComparer.Equals(cube.LeftFace, movedCube.FrontFace), Is.True);
        });
    }

    private static IEnumerable<TestCaseData> Get_WholeCubeMove_Should_YellowUpOrangeRightGreenFrontCube_TestCases()
    {
        for (var cubeDimension = StartCubeDimensionTest; cubeDimension <= EndCubeDimensionTest; cubeDimension++)
        {
            yield return new TestCaseData(cubeDimension);
        }
    }


    [TestCaseSource(nameof(Get_SliceMove_When_RepeatedSixTimesBangBang_Should_SolvedCube_TestCases))]
    public void SliceMove_When_RepeatedSixTimesBangBang_Should_SolvedCube(IEnumerable<MoveBase> moves)
    {
        var cube = _builder.BuildSolvedCube(3);

        var movedCube = _mover.Move(cube, moves);

        Assert.That(_cubeEqualityComparer.Equals(cube, movedCube), Is.True);
    }

    private static IEnumerable<TestCaseData> Get_SliceMove_When_RepeatedSixTimesBangBang_Should_SolvedCube_TestCases()
    {
        var moveFacePairs = MoveFaces.SelectMany(moveFace1 =>
            MoveFaces.Where(moveFace2 => moveFace1 != moveFace2)
                .Select(moveFace2 => new { MoveFace1 = moveFace1, MoveFace2 = moveFace2 }));

        var moveDirectionPairs = MoveDirections.SelectMany(moveDirection1 =>
            MoveDirections.Select(moveDirection2 =>
                new { MoveDirection1 = moveDirection1, MoveDirection2 = moveDirection2 }));

        var bangBangs = moveFacePairs.SelectMany(moveFacePair =>
            moveDirectionPairs.Select(moveDirectionPair =>
                new[]
                {
                    new SliceMove(moveFacePair.MoveFace1, moveDirectionPair.MoveDirection1, 0),
                    new SliceMove(moveFacePair.MoveFace2, moveDirectionPair.MoveDirection2, 0),
                    new SliceMove(moveFacePair.MoveFace1, OppositeMoveDirectionProvider.GetOppositeMoveDirection(moveDirectionPair.MoveDirection1), 0),
                    new SliceMove(moveFacePair.MoveFace2, OppositeMoveDirectionProvider.GetOppositeMoveDirection(moveDirectionPair.MoveDirection2), 0),
                }));

        var repeatedSixTimesBangBangsWithName = bangBangs.Select(bangBang =>
            new
            {
                Moves = Enumerable.Repeat(bangBang, 6).SelectMany(moves => moves),
                Name = RubiksCubeMovesConverter.RubiksCubeMovesToString(bangBang)
            } );

        foreach (var repeatedSixTimesBangBangWithName in repeatedSixTimesBangBangsWithName)
        {
            var testCase = new TestCaseData(repeatedSixTimesBangBangWithName.Moves);

            testCase.SetName($"{nameof(SliceMove_When_RepeatedSixTimesBangBang_Should_SolvedCube)}" +
                             $"(bangBang: {repeatedSixTimesBangBangWithName.Name})");

            yield return testCase;
        }
    }


    [TestCaseSource(nameof(Get_MoveSingle_Should_ChessPattern_TestCases))]
    public void MoveSingle_Should_ChessPattern(int cubeDimension, IEnumerable<MoveBase> moves)
    {
        var cube = _builder.BuildSolvedCube(cubeDimension);
        var movedCube = _builder.BuildSolvedCube(cubeDimension);

        movedCube = moves.Aggregate(movedCube, _mover.Move);

        Assert.Multiple(() =>
        {
            for (var i = 0; i < cubeDimension; i++)
            {
                for (var j = 0; j < cubeDimension; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        Assert.That(cube.UpFace.StickerColors[i][j], Is.EqualTo(movedCube.UpFace.StickerColors[i][j]));
                        Assert.That(cube.RightFace.StickerColors[i][j], Is.EqualTo(movedCube.RightFace.StickerColors[i][j]));
                        Assert.That(cube.FrontFace.StickerColors[i][j], Is.EqualTo(movedCube.FrontFace.StickerColors[i][j]));

                        Assert.That(cube.DownFace.StickerColors[i][j], Is.EqualTo(movedCube.DownFace.StickerColors[i][j]));
                        Assert.That(cube.LeftFace.StickerColors[i][j], Is.EqualTo(movedCube.LeftFace.StickerColors[i][j]));
                        Assert.That(cube.BackFace.StickerColors[i][j], Is.EqualTo(movedCube.BackFace.StickerColors[i][j]));
                    }
                    else
                    {
                        Assert.That(cube.UpFace.StickerColors[i][j], Is.EqualTo(movedCube.DownFace.StickerColors[i][j]));
                        Assert.That(cube.RightFace.StickerColors[i][j], Is.EqualTo(movedCube.LeftFace.StickerColors[i][j]));
                        Assert.That(cube.FrontFace.StickerColors[i][j], Is.EqualTo(movedCube.BackFace.StickerColors[i][j]));

                        Assert.That(cube.DownFace.StickerColors[i][j], Is.EqualTo(movedCube.UpFace.StickerColors[i][j]));
                        Assert.That(cube.LeftFace.StickerColors[i][j], Is.EqualTo(movedCube.RightFace.StickerColors[i][j]));
                        Assert.That(cube.BackFace.StickerColors[i][j], Is.EqualTo(movedCube.FrontFace.StickerColors[i][j]));
                    }
                }
            }
        });
    }

    private static IEnumerable<TestCaseData> Get_MoveSingle_Should_ChessPattern_TestCases()
    {
        for (var cubeDimension = StartCubeDimensionTest; cubeDimension <= EndCubeDimensionTest; cubeDimension++)
        {
            if (cubeDimension % 2 == 0) continue;

            var sliceNumbers = Enumerable.Range(0, cubeDimension).Where(x => x % 2 == 1).ToList();

            var rightMoves = sliceNumbers.Select(sliceNumber =>
                new SliceMove(MoveFace.Right, MoveDirection.Clockwise, sliceNumber)).ToList();

            var frontMoves = sliceNumbers .Select(sliceNumber =>
                new SliceMove(MoveFace.Front, MoveDirection.Clockwise, sliceNumber)).ToList();

            var upMoves = sliceNumbers .Select(sliceNumber =>
                new SliceMove(MoveFace.Up, MoveDirection.Clockwise, sliceNumber)).ToList();

            var sliceMoves = rightMoves.Concat(rightMoves)
                .Concat(frontMoves).Concat(frontMoves)
                .Concat(upMoves).Concat(upMoves);

            var wholeCubeMoves = AxisNames.SelectMany(axis => new[]
            {
                new WholeMove(axis, MoveDirection.Counterclockwise),
                new WholeMove(axis, MoveDirection.Counterclockwise),
            });

            var moves = sliceMoves.Select(sliceMove => (MoveBase)sliceMove)
                .Concat(wholeCubeMoves);

            var testCase = new TestCaseData(cubeDimension, moves);
            testCase.SetName($"{nameof(MoveSingle_Should_ChessPattern)}(cubeDimension: {cubeDimension})");

            yield return testCase;
        }
    }
}