using System.Reactive.Disposables;
using Avalonia.Controls.Primitives;

namespace ScrollViewerTest.EnhancedScrollViewer;


public class ScrollBarClientAreaExpansionStrategy : ScrollViewerExpansionStrategy
{
    private readonly CompositeDisposable disposables = new();

    public ScrollBarClientAreaExpansionStrategy(ScrollBar scrollBar)
    {
        ExpandOnHover(scrollBar, scrollBar).DisposeWith(disposables);
    }

    public override void Dispose()
    {
        disposables.Dispose();
    }
}