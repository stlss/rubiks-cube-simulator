using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

public sealed class RubiksCubeStickerControlViewModel : ObservableObject
{
    private SolidColorBrush _stickerColorBrush = new(Colors.DarkGray);

    public SolidColorBrush StickerColorBrush
    {
        get => _stickerColorBrush;
        set => SetProperty(ref _stickerColorBrush, value);
    }

    private ArrowDirection? _arrowDirection = null;

    public ArrowDirection? ArrowDirection
    {
        get => _arrowDirection;
        internal set
        {
            if (SetProperty(ref _arrowDirection, value))
            {
                OnPropertyChanged(nameof(MoveArrowVisibility));
                OnPropertyChanged(nameof(MoveArrowAngle));
            }
        }
    }

    public Visibility MoveArrowVisibility => ArrowDirection == null
        ? Visibility.Hidden
        : Visibility.Visible;

    public double MoveArrowAngle => ArrowDirection switch
    {
        RubiksCube.ArrowDirection.Left => 0,
        RubiksCube.ArrowDirection.Top => 90,
        RubiksCube.ArrowDirection.Right => 180,
        RubiksCube.ArrowDirection.Bottom => 270,
        null => 0,

        _ => throw new ArgumentOutOfRangeException()
    };
}

public enum ArrowDirection
{
    Left,
    Top,
    Right,
    Bottom,
}