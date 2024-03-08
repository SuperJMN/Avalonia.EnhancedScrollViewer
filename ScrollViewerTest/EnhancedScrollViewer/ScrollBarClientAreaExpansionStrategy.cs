using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Controls.Primitives;
using Avalonia.ReactiveUI;

namespace ScrollViewerTest.EnhancedScrollViewer;

public class ScrollBarClientAreaExpansionStrategy : ScrollViewerExpansionStrategy
{
    private readonly CompositeDisposable disposables = new();

    public ScrollBarClientAreaExpansionStrategy(ScrollBar scrollBar)
    {
        var isExpandedProperty = typeof(ScrollBar).GetProperty("IsExpanded");

        if (isExpandedProperty is null)
        {
            return;
        }

        Observable.FromEventPattern(scrollBar, "PointerEntered")
        .Do(_ =>
        {
            isExpandedProperty.SetValue(scrollBar, true);
        })
        .Subscribe()
        .DisposeWith(disposables);

        var hideAfter = scrollBar.GetValue(Expansion.HideAfterProperty);
        
        Observable.FromEventPattern(scrollBar, "PointerExited")
            .Delay(hideAfter, AvaloniaScheduler.Instance)
            .Do(_ =>
            {
                isExpandedProperty.SetValue(scrollBar, false);
            })
            .Subscribe()
            .DisposeWith(disposables);
    }

    public override void Dispose()
    {
        disposables.Dispose();
    }
}