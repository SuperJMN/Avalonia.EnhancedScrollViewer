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

        var observable = parentScrollViewer.GetObservable(Expansion.AreaProperty);
        observable
            .Select(GetExpansionType)
            .Select(x => CreateStrategy(AssociatedObject!, x))
            .Do(s => serialDisposable.Disposable = s)
            .Subscribe()
            .DisposeWith(disposable);
    }

    private static Type GetExpansionType(ExpansionType expansionType)
    {
        return expansionType switch
        {
            ExpansionType.ScrollBar => typeof(ScrollBarClientAreaExpansionStrategy),
            ExpansionType.ScrollViewer => typeof(ScrollViewerClientAreaExpansionStrategy),
            _ => throw new ArgumentOutOfRangeException(nameof(expansionType), expansionType, null)
        };
    }
}