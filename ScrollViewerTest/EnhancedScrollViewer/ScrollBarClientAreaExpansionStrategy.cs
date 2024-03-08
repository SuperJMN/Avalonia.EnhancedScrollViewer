﻿using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Controls.Primitives;

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

        Observable.FromEventPattern(scrollBar, "PointerExited")
            .Delay(TimeSpan.FromSeconds(1), Scheduler.CurrentThread)
            .Do(_ =>
            {
                isExpandedProperty.SetValue(scrollBar, false);
            })
            .Subscribe()
            .DisposeWith(disposables);;
    }

    public override void Dispose()
    {
        disposables.Dispose();
    }
}