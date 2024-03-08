using System;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using ReactiveUI;

namespace ScrollViewerTest.EnhancedScrollViewer;

public class ScrollViewerClientAreaExpansionStrategy : ScrollViewerExpansionStrategy
{
    private readonly ScrollBar scrollBar;

    public ScrollViewerClientAreaExpansionStrategy(ScrollBar scrollBar)
    {
        this.scrollBar = scrollBar;

        var isExpandedProperty = typeof(ScrollBar).GetProperty("IsExpanded");

        this.WhenAnyValue(x => x.scrollBar.TemplatedParent)
            .OfType<ScrollViewer>()
            .Select(x => Observable.FromEventPattern(x, "PointerEntered"))
            .Switch()
            .Do(pattern =>
            {
                isExpandedProperty.SetValue(scrollBar, true);
            })
            .Subscribe();

        this.WhenAnyValue(x => x.scrollBar.TemplatedParent)
            .OfType<ScrollViewer>()
            .Select(x => Observable.FromEventPattern(x, "PointerExited"))
            .Switch()
            .Do(pattern =>
            {
                isExpandedProperty.SetValue(scrollBar, false);
            })
            .Subscribe();
    }

    public override void Dispose()
    {
    }
}