using System.Windows;
using System.Windows.Input;
using RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers.MoveRubiksCubeEventHandlers.EventArgs;

namespace RubiksCubeSimulator.Wpf.App.Infrastructure.EventHandlers;

internal interface IMainWindowEventHandler
{
    public void OnKeyDown(object? sender, KeyEventArgs e);

    public void OnKeyUp(object? sender, KeyEventArgs e);


    public event EventHandler<MovingRubiksCubeEventArgs>? MovingRubiksCube;

    public event EventHandler<MovedRubiksCubeEventArgs>? MovedRubiksCube;
}

internal sealed class MainWindowEventHandler(IRubiksCubeControlEventHandler cubeEventHandler)
    : IMainWindowEventHandler
{
    private static readonly HashSet<Key> MoveKeys = [Key.W, Key.A, Key.S, Key.D];

    private FaceName? _faceName;
    private int? _stickerNumber;
    private Point? _relativeMousePosition;

    private Key? _pressedMoveKey;
    private bool _pressedShift;


    public void OnKeyDown(object? sender, KeyEventArgs e)
    {
        var key = e.Key;

        if (key == Key.LeftShift || key == Key.RightShift)
        {
            _pressedShift = true;
            SetCubeEventHandlerProperties();

            if (_pressedMoveKey != null && !IsCubeEventHandlerPropertiesNull())
            {
                var eventArgs = CreateMovingRubiksCubeEventArgs();
                MovingRubiksCube?.Invoke(this, eventArgs);
            }
        }

        else if (_pressedMoveKey == null && MoveKeys.Contains(key))
        {
            _pressedMoveKey = key;
            SetCubeEventHandlerProperties();

            if (!IsCubeEventHandlerPropertiesNull())
            {
                var eventArgs = CreateMovingRubiksCubeEventArgs();
                MovingRubiksCube?.Invoke(this, eventArgs);
            }
        }
    }

    public void OnKeyUp(object? sender, KeyEventArgs e)
    {
        var key = e.Key;

        if (key == Key.LeftShift || key == Key.RightShift)
        {
            _pressedShift = false;

            if (_pressedMoveKey != null && !IsCubeEventHandlerPropertiesNull())
            {
                var eventArgs = CreateMovingRubiksCubeEventArgs();
                MovingRubiksCube?.Invoke(this, eventArgs);
            }
        }

        else if (key == _pressedMoveKey)
        {
            if (!IsCubeEventHandlerPropertiesNull())
            {
                var eventArgs = CreateMovedRubiksCubeEventArgs();
                MovedRubiksCube?.Invoke(this, eventArgs);
            }

            _pressedMoveKey = null;
        }
    }


    private void SetCubeEventHandlerProperties()
    {
        _faceName = cubeEventHandler.FaceName;
        _stickerNumber = cubeEventHandler.StickerNumber;
        _relativeMousePosition = cubeEventHandler.RelativeMouserPosition;
    }

    private bool IsCubeEventHandlerPropertiesNull()
    {
        return !(_faceName.HasValue && _stickerNumber.HasValue && _relativeMousePosition.HasValue);
    }


    public event EventHandler<MovingRubiksCubeEventArgs>? MovingRubiksCube;

    public event EventHandler<MovedRubiksCubeEventArgs>? MovedRubiksCube;


    private MovingRubiksCubeEventArgs CreateMovingRubiksCubeEventArgs()
    {
        return new MovingRubiksCubeEventArgs
        {
            FaceName = _faceName!.Value,
            StickerNumber = _stickerNumber!.Value,
            RelativeMousePosition = _relativeMousePosition!.Value,
            PressedMoveKey = _pressedMoveKey!.Value,
            PressedShift = _pressedShift,
        };
    }

    private MovedRubiksCubeEventArgs CreateMovedRubiksCubeEventArgs()
    {
        return new MovedRubiksCubeEventArgs
        {
            FaceName = _faceName!.Value,
            StickerNumber = _stickerNumber!.Value,
            RelativeMousePosition = _relativeMousePosition!.Value,
            PressedMoveKey = _pressedMoveKey!.Value,
            PressedShift = _pressedShift,
        };
    }
}