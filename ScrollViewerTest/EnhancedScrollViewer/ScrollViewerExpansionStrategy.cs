using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.ReactiveUI;

namespace ScrollViewerTest.EnhancedScrollViewer;

public abstract class ScrollViewerExpansionStrategy : IDisposable
{
    protected static IDisposable ExpandOnHover(ScrollBar scrollBar, AvaloniaObject mouseTarget)
    {
        var isExpandedProperty = typeof(ScrollBar).GetProperty("IsExpanded");

        if (isExpandedProperty is null)
        {
            return Disposable.Empty;
        }

        var hideAfter = scrollBar.GetValue(Expansion.HideAfterProperty);
        var pointerEnter = Observable.FromEventPattern(mouseTarget, "PointerEntered").Select(pattern => Observable.Return(true));
        var pointerExit = Observable.FromEventPattern(mouseTarget, "PointerExited").Select(_ => Observable.Return(false).Delay(hideAfter, AvaloniaScheduler.Instance));

        var isExpanded = pointerEnter.Merge(pointerExit).Switch();

        return isExpanded
            .Do(b => isExpandedProperty.SetValue(scrollBar, b))
            .Subscribe();
    }
    
    public abstract void Dispose();
}