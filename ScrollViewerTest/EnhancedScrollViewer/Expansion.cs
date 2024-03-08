using System;
using Avalonia;
using Avalonia.Controls;

namespace ScrollViewerTest.EnhancedScrollViewer;

public class Expansion
{
    /// <summary>
    /// Defines which area triggers the ScrollBars expansion.
    /// </summary>
    public static readonly AttachedProperty<ExpansionArea> HotAreaProperty = AvaloniaProperty.RegisterAttached<Expansion, ScrollViewer, ExpansionArea>("Area");
    public static void SetHotArea(ScrollViewer obj, ExpansionArea value) => obj.SetValue(HotAreaProperty, value);
    public static ExpansionArea GetHotArea(ScrollViewer obj) => obj.GetValue(HotAreaProperty);

    /// <summary>
    /// Hides ScrollBars after a given <see cref="TimeSpan"/>.
    /// </summary>
    public static readonly AttachedProperty<TimeSpan> HideAfterProperty = AvaloniaProperty.RegisterAttached<Expansion, ScrollViewer, TimeSpan>("HideAfter", defaultValue: TimeSpan.FromSeconds(1), inherits: true);
    public static void SetHideAfter(ScrollViewer obj, TimeSpan value) => obj.SetValue(HideAfterProperty, value);
    public static TimeSpan GetHideAfter(ScrollViewer obj) => obj.GetValue(HideAfterProperty);
}