using System;
using Avalonia.Controls.Primitives;
using Avalonia.Xaml.Interactivity;

namespace ScrollViewerTest;

public class ScrollBarExpansionBehavior : Behavior<ScrollBar>
{
    private ScrollViewerExpansionStrategy? strategy;
    
    protected override void OnAttachedToVisualTree()
    {
        base.OnAttachedToVisualTree();

        if (Strategy is null)
        {
            return;
        }
        
        var scrollBarHideStrategy = Activator.CreateInstance(Strategy, AssociatedObject) as ScrollViewerExpansionStrategy;
        strategy = scrollBarHideStrategy ?? throw new InvalidOperationException($"Could not create instance of Strategy (type: {Strategy}). ");
    }

    public Type? Strategy { get; set; }

    protected override void OnDetachedFromVisualTree()
    {
        strategy?.Dispose();
        base.OnDetachedFromVisualTree();
    }
}

public abstract class ScrollViewerExpansionStrategy : IDisposable
{
    public abstract void Dispose();
}