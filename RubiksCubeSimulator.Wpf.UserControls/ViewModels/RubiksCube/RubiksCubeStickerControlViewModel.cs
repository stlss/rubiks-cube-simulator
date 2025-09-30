using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube.Enums;
using RubiksCubeSimulator.Wpf.UserControls.Views.Behaviors;

namespace RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

public sealed class RubiksCubeStickerControlViewModel : ObservableObject
{
    public int StickerNumber { get; init; }


    private SolidColorBrush _stickerColorBrush = new(Colors.DarkGray);

    public SolidColorBrush StickerColorBrush
    {
        get => _stickerColorBrush;
        set => SetProperty(ref _stickerColorBrush, value);
    }


    private ArrowDirection? _arrowDirection;

    public ArrowDirection? ArrowDirection
    {
        get => _arrowDirection;
        set
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
        Enums.ArrowDirection.Left => 0,
        Enums.ArrowDirection.Top => 90,
        Enums.ArrowDirection.Right => 180,
        Enums.ArrowDirection.Bottom => 270,
        null => 0,

        _ => throw new ArgumentOutOfRangeException()
    };


    private Point? _relativeMousePosition;

    public Point? RelativeMousePosition
    {
        get => _relativeMousePosition;
        private set => SetProperty(ref _relativeMousePosition, value);
    }

    public IRelayCommand<RelativeMouseEventArgs> UpdateRelativeMousePositionCommand { get; }


    public RubiksCubeStickerControlViewModel()
    {
        UpdateRelativeMousePositionCommand = new RelayCommand<RelativeMouseEventArgs>(eventArgs =>
        {
            RelativeMousePosition = eventArgs!.RelativeMousePosition;
        });
    }
}