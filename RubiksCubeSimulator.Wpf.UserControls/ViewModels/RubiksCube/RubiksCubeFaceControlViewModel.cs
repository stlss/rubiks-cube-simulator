using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Events.EventArgs;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube.Enums;

namespace RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

public sealed class RubiksCubeFaceControlViewModel :
    ObservableObject,
    IPublisher<MouseMoveRubiksCubeEventArgs>,
    IDisposable
{
    public FaceName FaceName { get; init; }


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


    private readonly List<ISubscriber<MouseMoveRubiksCubeEventArgs>> _mouseMoveSubscribers = [];

    public void Subscribe(ISubscriber<MouseMoveRubiksCubeEventArgs> subscriber)
    {
        _mouseMoveSubscribers.Add(subscriber);
    }

    public void Unsubscribe(ISubscriber<MouseMoveRubiksCubeEventArgs> subscriber)
    {
        _mouseMoveSubscribers.Remove(subscriber);
    }

    private void NotifySubscribers(MouseMoveRubiksCubeEventArgs mouseMoveEventArgs)
    {
        foreach (var subscriber in _mouseMoveSubscribers) subscriber.OnEvent(this, mouseMoveEventArgs);
    }


    private readonly List<PropertyChangedEventHandler> _relativeMousePositionPropertyChangedHandlers = new();

    private void SubscribeRelativeMousePositionPropertyChangedHandlers()
    {
        foreach (var stickerViewModel in _stickerViewModels)
        {
            void OnRelativeMousePositionPropertyChanged(object? sender,
                PropertyChangedEventArgs propertyChangedEventArgs)
            {
                var propertyName = nameof(RubiksCubeStickerControlViewModel.RelativeMousePosition);

                if (propertyChangedEventArgs.PropertyName != propertyName)
                    return;

                var mouseMoveEventArgs = new MouseMoveRubiksCubeEventArgs
                {
                    FaceName = FaceName,
                    StickerNumber = stickerViewModel.StickerNumber,
                    RelativeMousePosition = stickerViewModel.RelativeMousePosition
                };

                NotifySubscribers(mouseMoveEventArgs);
            }

            stickerViewModel.PropertyChanged += OnRelativeMousePositionPropertyChanged;
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