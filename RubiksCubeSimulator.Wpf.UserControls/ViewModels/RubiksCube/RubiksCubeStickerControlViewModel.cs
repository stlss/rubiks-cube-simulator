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
}