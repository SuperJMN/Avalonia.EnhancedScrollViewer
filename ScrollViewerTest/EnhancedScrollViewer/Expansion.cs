using Avalonia;
using Avalonia.Controls;

namespace ScrollViewerTest.EnhancedScrollViewer;

public class Expansion
{
    public static readonly AttachedProperty<ExpansionType> AreaProperty = AvaloniaProperty.RegisterAttached<Expansion, ScrollViewer, ExpansionType>("Area");
    public static void SetArea(ScrollViewer obj, ExpansionType value) => obj.SetValue(AreaProperty, value);
    public static ExpansionType GetArea(ScrollViewer obj) => obj.GetValue(AreaProperty);
}