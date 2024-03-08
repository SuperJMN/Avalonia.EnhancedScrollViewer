using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Avalonia.Controls.Primitives;
using ReactiveUI;

namespace ScrollViewerTest.EnhancedScrollViewer;

public class ScrollBarClientAreaExpansionStrategy : ScrollViewerExpansionStrategy
{
    private readonly ScrollBar scrollBar;

    public ScrollBarClientAreaExpansionStrategy(ScrollBar scrollBar)
    {
        this.scrollBar = scrollBar;

        var isExpandedProperty = typeof(ScrollBar).GetProperty("IsExpanded");

        this.WhenAnyValue(x => x.scrollBar)
            .Select(x => Observable.FromEventPattern(x, "PointerEntered"))
            .Switch()
            .Do(pattern =>
            {
                isExpandedProperty.SetValue(scrollBar, true);
            })
            .Subscribe();

        this.WhenAnyValue(x => x.scrollBar)
            .Select(x => Observable.FromEventPattern(x, "PointerExited").Delay(TimeSpan.FromSeconds(1), Scheduler.CurrentThread))
            .Switch()
            .Do(_ =>
            {
                isExpandedProperty.SetValue(scrollBar, false);
            })
            .Subscribe();
    }

    public override void Dispose()
    {
    }
}