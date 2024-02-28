using System;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;
using Zafiro.Avalonia.Mixins;

namespace ScrollViewerTest;

public class FullClientAreScrollBarExpansion : Behavior<ScrollViewer>
{
    private CustomScrollBar? verticalScrollBar;

    public FullClientAreScrollBarExpansion()
    {
        
    }

    protected override void OnAttached()
    {
        AssociatedObject.TemplateApplied += AssociatedObjectOnTemplateApplied;
    }

    private void AssociatedObjectOnTemplateApplied(object? sender, TemplateAppliedEventArgs e)
    {
        verticalScrollBar = (CustomScrollBar)e.NameScope.Find("PART_VerticalScrollBar");
    }

    protected override void OnAttachedToVisualTree()
    {
        base.OnAttachedToVisualTree();
        
        AssociatedObject.TemplateApplied += AssociatedObjectOnTemplateApplied;

        Observable.FromEventPattern(AssociatedObject, "PointerEntered")
            .Subscribe(pattern =>
            {
                ExpandScrollBars();
            });
        
        Observable.FromEventPattern(AssociatedObject, "PointerExited")
            .Subscribe(pattern =>
            {
                CollapseScrollBars();
            });
        
        AssociatedObject.OnEvent(InputElement.PointerEnteredEvent, RoutingStrategies.Bubble).Subscribe(pattern => { });
    }

    private void ExpandScrollBars()
    {
        var prop = typeof(ScrollBar).GetProperty("IsExpanded");
        prop.SetValue(verticalScrollBar, true);
        var currrent = prop.GetValue(verticalScrollBar);
        //verticalScrollBar.IsExpanded = true;
    }
    
    private void CollapseScrollBars()
    {
        var prop = typeof(ScrollBar).GetProperty("IsExpanded");
        prop.SetValue(verticalScrollBar, false);
        var currrent = prop.GetValue(verticalScrollBar);
        //verticalScrollBar.IsExpanded = true;
    }
}