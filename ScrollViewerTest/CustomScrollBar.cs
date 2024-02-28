using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace ScrollViewerTest;

public class CustomScrollBar : ScrollBar
{
    protected override void OnPointerEntered(PointerEventArgs e)
    {
        base.OnPointerEntered(e);
    }

    protected override void OnPointerExited(PointerEventArgs e)
    {
        base.OnPointerExited(e);
    }
}