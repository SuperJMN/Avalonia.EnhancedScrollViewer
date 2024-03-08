using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.VisualTree;
using Avalonia.Xaml.Interactions.Custom;

namespace ScrollViewerTest.EnhancedScrollViewer;

public class ScrollBarExpansionBehavior : AttachedToVisualTreeBehavior<ScrollBar>
{
    private readonly SerialDisposable serialDisposable = new();

    private static ScrollViewerExpansionStrategy CreateStrategy(ScrollBar scrollBar, Type strategyType)
    {
        var scrollBarHideStrategy = Activator.CreateInstance(strategyType, scrollBar) as ScrollViewerExpansionStrategy;
        return scrollBarHideStrategy ?? throw new InvalidOperationException($"Could not create instance of Strategy (type: {strategyType}). ");
    }

    public static readonly StyledProperty<Type?> StrategyTypeProperty = AvaloniaProperty.Register<ScrollBarExpansionBehavior, Type?>(nameof(StrategyType));

    public Type? StrategyType
    {
        get => GetValue(StrategyTypeProperty);
        set => SetValue(StrategyTypeProperty, value);
    }

    protected override void OnAttachedToVisualTree(CompositeDisposable disposable)
    {
        var parentScrollViewer = AssociatedObject.FindAncestorOfType<ScrollViewer>();

        if (parentScrollViewer is null)
        {
            return;
        }

        serialDisposable.DisposeWith(disposable);

        parentScrollViewer.GetObservable(Expansion.HotAreaProperty)
            .Select(GetExpansionType)
            .Select(x => CreateStrategy(AssociatedObject!, x))
            .Do(s => serialDisposable.Disposable = s)
            .Subscribe()
            .DisposeWith(disposable);
    }

    private static Type GetExpansionType(ExpansionArea expansionArea)
    {
        return expansionArea switch
        {
            ExpansionArea.ScrollBar => typeof(ScrollBarClientAreaExpansionStrategy),
            ExpansionArea.ScrollViewer => typeof(ScrollViewerClientAreaExpansionStrategy),
            _ => throw new ArgumentOutOfRangeException(nameof(expansionArea), expansionArea, null)
        };
    }
}