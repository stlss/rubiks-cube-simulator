using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

public sealed class RubiksCubeFaceControlViewModel : ObservableObject, IDisposable
{
    private const int DefaultDimension = 3;

    public int CubeDimension { get; init; } = DefaultDimension;

    public Thickness BorderThickness => new(1.5 * DefaultDimension / CubeDimension);


    private readonly IReadOnlyList<RubiksCubeStickerControlViewModel> _stickerViewModels = Enumerable
        .Range(0, DefaultDimension * DefaultDimension)
        .Select(_ => new RubiksCubeStickerControlViewModel())
        .ToList();

    public IReadOnlyList<RubiksCubeStickerControlViewModel> StickerViewModels
    {
        get => _stickerViewModels;
        init
        {
            UnsubscribeRelativeMousePositionPropertyChangedHandlers();
            _stickerViewModels = value;
            SubscribeRelativeMousePositionPropertyChangedHandlers();
        }
    }


    public RubiksCubeFaceControlViewModel()
    {
        SubscribeRelativeMousePositionPropertyChangedHandlers();
    }


    internal void SetMoveArrows(ArrowDirection arrowDirection)
    {
        foreach (var stickerViewModel in StickerViewModels) stickerViewModel.ArrowDirection = arrowDirection;
    }

    internal void SetRowMoveArrows(ArrowDirection arrowDirection, int row)
    {
        var start = row * CubeDimension;
        var end = start + CubeDimension;

        for (var i = start; i < end; i++) StickerViewModels[i].ArrowDirection = arrowDirection;
    }

    internal void SetColumnMoveArrows(ArrowDirection arrowDirection, int column)
    {
        var start = column;
        var end = StickerViewModels.Count;
        var step = CubeDimension;

        for (var i = start; i < end; i += step) StickerViewModels[i].ArrowDirection = arrowDirection;
    }

    internal void ClearMoveArrows()
    {
        foreach (var stickerViewModel in StickerViewModels) stickerViewModel.ArrowDirection = null;
    }


    public event EventHandler<MouseMoveRubiksCubeFaceEventArgs>? MoveMouse;
    private readonly List<PropertyChangedEventHandler> _relativeMousePositionPropertyChangedHandlers = new();

    private void SubscribeRelativeMousePositionPropertyChangedHandlers()
    {
        var stickerNumbers = Enumerable.Range(0, _stickerViewModels.Count);

        var stickerViewModelsWithNumbers = _stickerViewModels
            .Zip(stickerNumbers, (viewModel, number) => (ViewModel: viewModel, Number: number));

        foreach (var (viewModel, number) in stickerViewModelsWithNumbers)
        {
            void OnRelativeMousePositionPropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName != nameof(RubiksCubeStickerControlViewModel.RelativeMousePosition))
                    return;

                MoveMouse?.Invoke(this, new MouseMoveRubiksCubeFaceEventArgs
                {
                    StickerNumber = viewModel.RelativeMousePosition != null ? number : null,
                    RelativeMousePosition = viewModel.RelativeMousePosition
                });
            }

            viewModel.PropertyChanged += OnRelativeMousePositionPropertyChanged;
            _relativeMousePositionPropertyChangedHandlers.Add(OnRelativeMousePositionPropertyChanged);
        }
    }

    private void UnsubscribeRelativeMousePositionPropertyChangedHandlers()
    {
        var stickerViewModelsWithHandlers = _stickerViewModels
            .Zip(_relativeMousePositionPropertyChangedHandlers,
                (viewModel, handler) => (ViewModel: viewModel, Handler: handler));

        foreach (var (viewModel, handler) in stickerViewModelsWithHandlers)
        {
            viewModel.PropertyChanged -= handler;
        }

        _relativeMousePositionPropertyChangedHandlers.Clear();
    }


    public void Dispose()
    {
        UnsubscribeRelativeMousePositionPropertyChangedHandlers();
    }
}

public sealed class MouseMoveRubiksCubeFaceEventArgs : EventArgs
{
    public required int? StickerNumber { get; init; }

    public required Point? RelativeMousePosition { get; init; }
}