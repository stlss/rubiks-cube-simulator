using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using RubiksCubeSimulator.Wpf.Events;
using RubiksCubeSimulator.Wpf.Events.EventArgs;
using RubiksCubeSimulator.Wpf.Events.EventArgs.Enums;
using RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube.Enums;

namespace RubiksCubeSimulator.Wpf.UserControls.ViewModels.RubiksCube;

public sealed class RubiksCubeFaceViewModel :
    ObservableObject,
    IPublisher<MouseMoveRubiksCubeEventArgs>,
    IDisposable
{
    public FaceName FaceName { get; init; }


    private const int DefaultDimension = 3;

    public int CubeDimension { get; init; } = DefaultDimension;


    private readonly IReadOnlyList<RubiksCubeStickerViewModel> _stickerViewModels = Enumerable
        .Range(0, DefaultDimension * DefaultDimension)
        .Select(_ => new RubiksCubeStickerViewModel())
        .ToList();

    public IReadOnlyList<RubiksCubeStickerViewModel> StickerViewModels
    {
        get => _stickerViewModels;
        init
        {
            UnsubscribeRelativeMousePositionPropertyChangedHandlers();
            _stickerViewModels = value;
            SubscribeRelativeMousePositionPropertyChangedHandlers();
        }
    }


    public RubiksCubeFaceViewModel()
    {
        SubscribeRelativeMousePositionPropertyChangedHandlers();
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
            stickerViewModel.PropertyChanged += OnRelativeMousePositionPropertyChanged;
            _relativeMousePositionPropertyChangedHandlers.Add(OnRelativeMousePositionPropertyChanged);

            continue;

            void OnRelativeMousePositionPropertyChanged(object? sender,
                PropertyChangedEventArgs propertyChangedEventArgs)
            {
                const string propertyName = nameof(RubiksCubeStickerViewModel.RelativeMousePosition);

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