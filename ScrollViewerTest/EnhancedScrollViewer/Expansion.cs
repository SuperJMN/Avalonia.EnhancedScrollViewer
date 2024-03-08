using System;
using Avalonia;
using Avalonia.Controls;

namespace ScrollViewerTest.EnhancedScrollViewer;

public class Expansion
{
    public static readonly AttachedProperty<ExpansionType> AreaProperty = AvaloniaProperty.RegisterAttached<Expansion, ScrollViewer, ExpansionType>("Area");
    public static void SetArea(ScrollViewer obj, ExpansionType value) => obj.SetValue(AreaProperty, value);
    public static ExpansionType GetArea(ScrollViewer obj) => obj.GetValue(AreaProperty);

    public static readonly AttachedProperty<TimeSpan> HideAfterProperty = AvaloniaProperty.RegisterAttached<Expansion, ScrollViewer, TimeSpan>("HideAfter", defaultValue: TimeSpan.FromSeconds(1), inherits: true);
    public static void SetHideAfter(ScrollViewer obj, TimeSpan value) => obj.SetValue(HideAfterProperty, value);
    public static TimeSpan GetHideAfter(ScrollViewer obj) => obj.GetValue(HideAfterProperty);
}